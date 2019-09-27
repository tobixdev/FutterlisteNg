using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FutterlisteNg.Web.Components
{
    public class FluentValidationValidator : ComponentBase
    {
        private static readonly char[] s_separators = {'.', '['};
        
        [Inject] private IServiceProvider ServiceProvider { get; set; }
        [Parameter] public Type ValidatorType { get; set; }
        [CascadingParameter] EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            if (EditContext == null)
                throw new InvalidOperationException("No Edit context found.");

            if (ValidatorType == null)
                throw new InvalidOperationException("No validator type specified.");

            var messages = new ValidationMessageStore(EditContext);
            
            EditContext.OnValidationRequested += (sender, eventArgs)
                => ValidateModel((EditContext) sender, messages);
        }

        private void ValidateModel(EditContext editContext, ValidationMessageStore messages)
        {
            var validationResult = GetValidator().Validate(editContext.Model);
            messages.Clear();
            foreach (var error in validationResult.Errors)
            {
                var fieldIdentifier = ToFieldIdentifier(editContext, error.PropertyName);
                messages.Add(fieldIdentifier, error.ErrorMessage);
            }

            editContext.NotifyValidationStateChanged();
        }

        // Thanks to https://gist.github.com/SteveSandersonMS/090145d7511c5190f62a409752c60d00
        private static FieldIdentifier ToFieldIdentifier(EditContext editContext, string propertyPath)
        {
            // This method parses property paths like 'SomeProp.MyCollection[123].ChildProp'
            // and returns a FieldIdentifier which is an (instance, propName) pair. For example,
            // it would return the pair (SomeProp.MyCollection[123], "ChildProp"). It traverses
            // as far into the propertyPath as it can go until it finds any null instance.

            var obj = editContext.Model;

            while (true)
            {
                var nextTokenEnd = propertyPath.IndexOfAny(s_separators);
                if (nextTokenEnd < 0)
                {
                    return new FieldIdentifier(obj, propertyPath);
                }

                var nextToken = propertyPath.Substring(0, nextTokenEnd);
                propertyPath = propertyPath.Substring(nextTokenEnd + 1);

                object newObj;
                if (nextToken.EndsWith("]"))
                {
                    // It's an indexer
                    // This code assumes C# conventions (one indexer named Item with one param)
                    nextToken = nextToken.Substring(0, nextToken.Length - 1);
                    var prop = obj.GetType().GetProperty("Item");
                    var indexerType = prop.GetIndexParameters()[0].ParameterType;
                    var indexerValue = Convert.ChangeType(nextToken, indexerType);
                    newObj = prop.GetValue(obj, new object[] {indexerValue});
                }
                else
                {
                    // It's a regular property
                    var prop = obj.GetType().GetProperty(nextToken);
                    if (prop == null)
                    {
                        throw new InvalidOperationException(
                            $"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
                    }

                    newObj = prop.GetValue(obj);
                }

                if (newObj == null)
                {
                    // This is as far as we can go
                    return new FieldIdentifier(obj, nextToken);
                }

                obj = newObj;
            }
        }

        private IValidator GetValidator()
        {
            return (IValidator) ServiceProvider.GetService(ValidatorType);
        }
    }
}