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
            var payload = new
            {
                html = $@"<i class=""material-icons"">cross</i> {message}",
                classes = "error-toast"
            };
            await _jsRuntime.InvokeVoidAsync("M.toast", payload);
        }

        public async Task Success(string message)
        {
            var payload = new
            {
                html = $@"<i class=""material-icons"">check</i> {message}",
                classes = "success-toast"
            };
            await _jsRuntime.InvokeVoidAsync("M.toast", payload);
        }
    }
}