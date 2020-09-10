using System;
using NumSharp;

namespace NumsharpOpencvSharpConvertor.Example
{
    class Program
    {
        static void Main(string[] args)
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
            NDArray array=new NDArray(data);
            Shape shape = array.Shape;
            int[] shapes = array.shape;
            Console.WriteLine("Hello World!");
        }
    }
}