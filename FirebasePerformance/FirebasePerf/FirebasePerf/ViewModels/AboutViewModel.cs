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

            OpenWebCommand = new Command(MakeHttpCall);
            TracedHttpCallCommand = new Command(MakeTracedHttpCall);
        }

        public ICommand OpenWebCommand { get; }
        public ICommand TracedHttpCallCommand { get; }

        private async void MakeHttpCall()
        {
#if __ANDROID__
            
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

        private async void MakeTracedHttpCall()
        {
#if __ANDROID__
            var metric = Firebase.Perf.FirebasePerformance.Instance.NewHttpMetric(new Java.Net.URL("https://jsonplaceholder.typicode.com/todos"), "GET");
            var client = new OkHttpClient();
            var requestBody = RequestBody.Create(
                MediaType.Parse("application/json"),
                "{\"title\":\"foo\",\"body\": \"bar\", \"userId\":\"1\", \"id\":\"1\"}");
            var request = new Request.Builder()
                .Url("https://jsonplaceholder.typicode.com/todos")
                .Post(requestBody)
                .Build();
            metric.Start();
            //metric.SetRequestPayloadSize()

            var response = await client.NewCall(request).ExecuteAsync();
            //set response payload size
            // set httpcode
            metric.Stop();
            Console.WriteLine("successful traced call made");

#endif
        }
    }
}