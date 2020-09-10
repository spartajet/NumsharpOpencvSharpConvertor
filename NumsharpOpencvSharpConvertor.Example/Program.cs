using System;
using NumSharp;
using OpenCvSharp;

namespace NumsharpOpencvSharpConvertor.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            NDArray array = Mat2NdArray();

            Mat mat = array.ToMat();
        }

        static NDArray Mat2NdArray()
        {
            Mat mat = Cv2.ImRead("Test2.png", ImreadModes.AnyDepth);
            NDArray array = mat.ToNdArray();
            return array;
        }

        static void NdArrayTest()
        {
            int[,,] data =
            {
                {
                    {1, 2, 3, 4},
                    {5, 6, 7, 8},
                    {9, 10, 11, 12}
                },
                {
                    {13, 14, 15, 16},
                    {17, 18, 19, 20},
                    {21, 22, 23, 24}
                }
            };
            NDArray array = new NDArray(data);
            Shape shape = array.Shape;
            int[] shapes = array.shape;
            Console.WriteLine("Hello World!");
        }
    }
}