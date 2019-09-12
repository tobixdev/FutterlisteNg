using Microsoft.JSInterop;

namespace FutterlisteNg.Web.Service
{
    public class ToastService : IToastService
    {
        private readonly IJSInProcessRuntime _jsRuntime;

        public ToastService(IJSRuntime jsRuntime)
        {
            _jsRuntime = (IJSInProcessRuntime) jsRuntime;
        }

        public void Error(string message)
        {
            var payload = new ErrorToast(message);
            _jsRuntime.InvokeVoid("M.toast", payload);
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