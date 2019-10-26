using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using LanguageAdventures.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace LanguageAdventures.iOS
{
    public class PhotoPickerService : IPhotoPickerService
    {
        TaskCompletionSource<Stream> taskCompletionSource;
        UIImagePickerController imagePicker;

        public Task<Stream> GetImageStreamAsync()
        {
            imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled += OnImagePickerCancelled;

            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(imagePicker, true);

            taskCompletionSource = new TaskCompletionSource<Stream>();
            return taskCompletionSource.Task;

            void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
            {
                UIImage image = args.EditedImage ?? args.OriginalImage;

                if (image != null)
                {
                    NSData data;
                    if (args.ReferenceUrl.PathExtension.Equals("PNG") || args.ReferenceUrl.PathExtension.Equals("png"))
                    {
                        data = image.AsPNG();
                    }
                    else
                    {
                        data = image.AsJPEG(1);
                    }
                    Stream stream = data.AsStream();

                    UnregisterEventHandlers();

                    taskCompletionSource.SetResult(stream);
                }
                else
                {
                    UnregisterEventHandlers();
                    taskCompletionSource.SetResult(null);
                }
                imagePicker.DismissModalViewController(true);
            }

            void OnImagePickerCancelled(object sender, EventArgs args)
            {
                UnregisterEventHandlers();
                taskCompletionSource.SetResult(null);
                imagePicker.DismissModalViewController(true);
            }

            void UnregisterEventHandlers()
            {
                imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
                imagePicker.Canceled -= OnImagePickerCancelled;
            }
        }
    }
}