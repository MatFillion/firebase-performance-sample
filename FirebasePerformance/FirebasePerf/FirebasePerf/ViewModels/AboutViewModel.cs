using Square.OkHttp3;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace FirebasePerf.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
        public AboutViewModel()
        {
            Title = "LearnMoreWillDo HttpCall";

			HttpCallCommand = new Command(MakeHttpCall);
        }

        public ICommand HttpCallCommand { get; }

        private async void MakeHttpCall()
        {
#if __ANDROID__
            // This call should be automatically traced.

			var client = new OkHttpClient();
			var requestBody = RequestBody.Create(
				MediaType.Parse("application/json"),
				"{\"title\":\"foo\",\"body\": \"bar\", \"userId\":\"1\", \"id\":\"1\"}");
			var request = new Request.Builder()
				.Url("https://jsonplaceholder.typicode.com/todos")
				.Post(requestBody)
				.Build();

			var response = await client.NewCall(request).ExecuteAsync();

            Console.WriteLine("successful call made");

#endif
        }
    }
}