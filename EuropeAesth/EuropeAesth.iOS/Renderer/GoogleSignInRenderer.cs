using System;
using System.Threading.Tasks;
using EuropeAesth.iOS.Renderer;
using EuropeAesth.Renderer;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(GoogleiOSSignIn), typeof(GoogleSignInRenderer))]
namespace EuropeAesth.iOS.Renderer
{
    //public class GoogleSignInRenderer : PageRenderer, ISignInUIDelegate, ISignInDelegate
    //{
    //    TaskCompletionSource<string> _taskCompletionSource;

    //    public override void ViewDidLoad()
    //    {
            
    //        Custom.GoogleTasks.Instance.GetAccessToken = GetAccessToken;
    //        base.ViewDidLoad();
    //    }
    //    public Task<string> GetAccessToken()
    //    {
    //        _taskCompletionSource = new TaskCompletionSource<string>();
    //        SignIn.SharedInstance.UIDelegate = this;
    //        SignIn.SharedInstance.Delegate = this;
    //        SignIn.SharedInstance.Scopes = new string[] { Google.Apis.Tasks.v1.TasksService.Scope.Tasks };
    //        SignIn.SharedInstance.SignInUser();
    //        return _taskCompletionSource.Task;
    //    }
    //    public void DidSignIn(SignIn signIn, GoogleUser user, NSError error)
    //    {
    //        if (error != null)
    //        {
    //            _taskCompletionSource.SetException(new NSErrorException(error));
    //        }
    //        else
    //        {
    //            _taskCompletionSource.SetResult(user.Authentication.AccessToken);
    //        }
    //    }
    //}
}