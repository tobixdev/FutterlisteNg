using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FutterlisteNg.Web.Service
{
    public class ToastService : IToastService
    {
        private readonly IJSRuntime _jsRuntime;

        public ToastService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Error(string message)
        {
            var payload = new ErrorToast(message);
            // await _jsRuntime.InvokeVoidAsync("M.toast", payload);
        }

        private class ErrorToast
        {
            public ErrorToast(string message)
            {
                html = message;
            }

            public string html { get; set; }
        }
    }
}