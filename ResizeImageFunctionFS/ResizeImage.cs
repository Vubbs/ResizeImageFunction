using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;

namespace ResizeImageFunctionFS
{
    public class ResizeImage
    {
        [FunctionName("ResizeImage")]
        public void Run([BlobTrigger("sample-images/{name}", Connection = "StorageConnection")]Stream myBlob,
                        [Blob("imagecontainer/{name}", FileAccess.Write)] Stream imageResize)
        {
            using var imageFormat = Image.Load(myBlob);
            imageFormat.Mutate(x => x.Resize(500, 300));
            imageFormat.SaveAsPng(imageResize, new PngEncoder());
        }
    }
}
