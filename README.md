# NumsharpOpencvSharpConvertor
A convertor betwwen Numsharp NdArray and OpenCvSharp Mat 

Following the simple code:

```
static void Main(string[] args)
{
    NDArray array = Mat2NdArray();

    Mat mat = array.ToMat();
}

static NDArray Mat2NdArray()
{
    Mat mat = Cv2.ImRead("Test1.png");
    Bitmap bitmap = mat.ToBitmap();
    NDArray array = mat.ToNdArray();
    return array;
}
```
