using System;
using System.Runtime.InteropServices;
using NumSharp;
using OpenCvSharp;

namespace NumsharpOpencvSharpConvertor
{
    public static class Convertor
    {
        /// <summary>
        /// type depth constants
        /// </summary>
        public const int
            CV_8U = 0,
            CV_8S = 1,
            CV_16U = 2,
            CV_16S = 3,
            CV_32S = 4,
            CV_32F = 5,
            CV_64F = 6,
            CV_USRTYPE1 = 7;

        /// <summary>
        /// Convert Mat to NdArray
        /// </summary>
        /// <param name="mat">OpenCvSharp Mat object</param>
        /// <returns>NDArray object</returns>
        public static NDArray ToNdArray(this Mat mat)
        {
            
            NDArray array = np.zeros(1);
            int depth = mat.Depth();
            byte[] data = new byte[mat.Cols * mat.Rows * depth];
            Marshal.Copy(mat.DataStart, data, 0, data.Length);
            switch (depth)
            {
                case CV_8U:
                    array = new NDArray(data);
                    break;
                case CV_8S:
                    array = new NDArray(data);
                    break;
                case CV_16U:
                    ushort[] UshortData = new ushort[data.Length / 2];
                    Buffer.BlockCopy(data, 0, UshortData, 0, data.Length/2);
                    array = new NDArray(UshortData);
                    break;
                case CV_16S:
                    short[] shortData = new short[data.Length / 2];
                    Buffer.BlockCopy(data, 0, shortData, 0, data.Length);
                    array = new NDArray(shortData);
                    break;
                case CV_32S:
                    int[] intData = new int[data.Length / 4];
                    Buffer.BlockCopy(data, 0, intData, 0, data.Length);
                    array = new NDArray(intData);
                    break;
                case CV_32F:
                    float[] floatData = new float[data.Length / 4];
                    Buffer.BlockCopy(data, 0, floatData, 0, data.Length);
                    array = new NDArray(floatData);
                    // array = array.astype(np.float32, false);
                    break;
                case CV_64F:
                    double[] doubleData = new double[data.Length / 8];
                    Buffer.BlockCopy(data, 0, doubleData, 0, data.Length);
                    array = new NDArray(doubleData);
                    // array = array.astype(np.float64, false);
                    break;
                case CV_USRTYPE1:
                    array = np.zeros(1);
                    throw new Exception("Can not support User Type!");
            }

            int channel = mat.Channels();
            array = channel == 1 ? array.reshape(mat.Rows, mat.Cols) : array.reshape(mat.Rows, mat.Cols, channel);
            return array;
        }

        /// <summary>
        /// Convert NdArray to Mat
        /// </summary>
        /// <param name="array">NumSharp NDArray object</param>
        /// <returns>mmat object</returns>
        public static Mat ToMat(this NDArray array)
        {

            Shape shape = array.Shape;
            int dim = shape.NDim;
            int row = 0;
            int col = 0;
            int channel = 0;
            
            switch (dim)
            {
                case 2:
                    row = shape[0];
                    col = shape[1];
                    channel = 1;
                    break;
                case 3:
                    row = shape[0];
                    col = shape[1];
                    channel = shape[2];
                    break;
                default:
                    throw new Exception("Not Support Dim!"); ;
            }
            if (channel>4)
            {
                throw new Exception("Not Support Channel Count!"); ;
            }
            MatType matType;
            Mat mat;
            switch (array.typecode)
            {
                
                case NPTypeCode.Byte:
                    matType = MatType.CV_8UC(channel);
                    byte[] data = array.ToByteArray();
                    mat = new Mat(row, col, matType, data);
                    break;
                case NPTypeCode.Int16:
                    matType = MatType.CV_16SC(channel);
                    short[] shortData = array.ToArray<short>();
                    mat = new Mat(row, col, matType, shortData);
                    break;
                case NPTypeCode.UInt16:
                    matType = MatType.CV_16UC(channel);
                    ushort[] ushortData = array.ToArray<ushort>();
                    mat = new Mat(row, col, matType, ushortData);
                    break;
                case NPTypeCode.Int32:
                    matType = MatType.CV_32SC(channel);
                    int[] intData = array.ToArray<int>();
                    mat = new Mat(row, col, matType, intData);
                    break;
                case NPTypeCode.Single:
                    matType = MatType.CV_32FC(channel);
                    float[] floatData = array.ToArray<float>();
                    mat = new Mat(row, col, matType, floatData);
                    break;
                case NPTypeCode.Double:
                    matType = MatType.CV_64FC(channel);
                    double[] doubleData = array.ToArray<double>();
                    mat = new Mat(row, col, matType, doubleData);
                    break;
                case NPTypeCode.Decimal:
                case NPTypeCode.String:
                case NPTypeCode.Complex:
                case NPTypeCode.Empty:
                case NPTypeCode.Boolean:
                case NPTypeCode.Char:
                case NPTypeCode.UInt32:
                case NPTypeCode.Int64:
                case NPTypeCode.UInt64:
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            
            return mat;
        }
    }
}