using System;
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
            byte[] data = mat.ToBytes();
            NDArray array = new NDArray(data);
            int depth = mat.Depth();
            switch (depth)
            {
                case CV_8U:
                    array = array.astype(np.uint8, false);
                    break;
                case CV_8S:
                    array = array.astype(np.uint8, false);
                    break;
                case CV_16U:
                    array = array.astype(np.uint16, false);
                    break;
                case CV_16S:
                    array = array.astype(np.int16, false);
                    break;
                case CV_32S:
                    array = array.astype(np.int32, false);
                    break;
                case CV_32F:
                    array = array.astype(np.float32, false);
                    break;
                case CV_64F:
                    array = array.astype(np.float64, false);
                    break;
                case CV_USRTYPE1:
                    throw new Exception("Can not support User Type!");
                    break;
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
            switch (array.typecode)
            {
                
                case NPTypeCode.Byte:
                    matType = MatType.CV_8UC(dim);
                    break;
                case NPTypeCode.Int16:
                    matType = MatType.CV_16SC(dim);
                    break;
                case NPTypeCode.UInt16:
                    matType = MatType.CV_16UC(dim);
                    break;
                case NPTypeCode.Int32:
                    matType = MatType.CV_32SC(dim);
                    break;
                case NPTypeCode.Single:
                    matType = MatType.CV_32FC(dim);
                    break;
                case NPTypeCode.Double:
                    matType = MatType.CV_64FC(dim);
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
            byte[] data = array.ToByteArray();
            Mat mat = new Mat(row, col, matType, data);
            return null;
        }
    }
}