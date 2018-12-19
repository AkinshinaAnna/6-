using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGLib.Math
{
    public class Matrix3
    {
        private float[,] _data;

        public float this[int row, int column]
        {
            get
            {
                return _data[row, column];
            }
            set
            {
                _data[row, column] = value;
            }
        }


        private const int SIZE = 3;

        public Matrix3()
        {
            _data = new float[SIZE, SIZE];

            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                    _data[i, j] = 0;
        }

        public void Translate(float x, float y)
        {
            _data[0, 2] = x;
            _data[1, 2] = y;
        }

        public void Translate(Vector2 translation)
        {
            Translate(translation.X, translation.Y);
        }

        public void Scale(float x, float y)
        {
            _data[0, 0] = x;
            _data[1, 1] = y;

        }

        public void Scale(Vector2 scale)
        {
            Scale(scale.X, scale.Y);
        }

        public void Rotate(float angle)
        {
            float sin = (float)System.Math.Sin(angle);
            float cos = (float)System.Math.Cos(angle);

            _data[0, 0] = _data[1, 1] = cos;
            _data[0, 1] = -sin;
            _data[1, 0] = sin;
        }

        public void SetIdentity()
        {
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                    _data[i, j] = (i == j) ? 1 : 0;
        }

        public float GetDeterminant()
        {
            return 
                _data[0, 0] * (_data[1, 1] * _data[2, 2] - _data[2, 1] * _data[1, 2]) -
                _data[0, 1] * (_data[1, 0] * _data[2, 2] - _data[1, 2] * _data[2, 0]) +
                _data[0, 2] * (_data[1, 0] * _data[2, 1] - _data[1, 1] * _data[2, 0]);
        }

        public Matrix3 Inverse()
        {
            float det = GetDeterminant();

            if (det == 0)
                throw new Exception("Determinant can not equals 0");

            float inversedDet = 1 / det;

            Matrix3 result = new Matrix3();

            result[0, 0] = (_data[1, 1] * _data[2, 2] - _data[2, 1] * _data[1, 2]) * inversedDet;
            result[0, 1] = (_data[0, 2] * _data[2, 1] - _data[0, 1] * _data[2, 2]) * inversedDet;
            result[0, 2] = (_data[0, 1] * _data[1, 2] - _data[0, 2] * _data[1, 1]) * inversedDet;
            result[1, 0] = (_data[1, 2] * _data[2, 0] - _data[1, 0] * _data[2, 2]) * inversedDet;
            result[1, 1] = (_data[0, 0] * _data[2, 2] - _data[0, 2] * _data[2, 0]) * inversedDet;
            result[1, 2] = (_data[1, 0] * _data[0, 2] - _data[0, 0] * _data[1, 2]) * inversedDet;
            result[2, 0] = (_data[1, 0] * _data[2, 1] - _data[2, 0] * _data[1, 1]) * inversedDet;
            result[2, 1] = (_data[2, 0] * _data[0, 1] - _data[0, 0] * _data[2, 1]) * inversedDet;
            result[2, 2] = (_data[0, 0] * _data[1, 1] - _data[1, 0] * _data[0, 1]) * inversedDet;

            return result;
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            Matrix3 result = new Matrix3();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int u = 0; u < 3; u++)
                        result[i, j] += a[i, u] * b[u, j];

            return result;
        }

        public static Vector3 operator *(Matrix3 mat, Vector3 vec)
        {
            Vector3 result = new Vector3();

            result.X = mat[0, 0] * vec.X + mat[0, 1] * vec.Y + mat[0, 2] * vec.Z;
            result.Y = mat[1, 0] * vec.X + mat[1, 1] * vec.Y + mat[1, 2] * vec.Z;
            result.Z = mat[2, 0] * vec.X + mat[2, 1] * vec.Y + mat[2, 2] * vec.Z;

            return result;
        }

        public static Matrix3 GetIdentity()
        {
            Matrix3 result = new Matrix3();
            result.SetIdentity();

            return result;
        }

        public static Matrix3 GetTranslation(Vector2 translation)
        {
            return GetTranslation(translation.X, translation.Y);
        }

        public static Matrix3 GetRotation(float angle)
        {
            Matrix3 result = GetIdentity();
            result.Rotate(angle);

            return result;
        }

        public static Matrix3 GetScale(Vector2 scale)
        {
            return GetTranslation(scale.X, scale.Y);
        }

        public static Matrix3 GetTranslation(float x, float y)
        {
            Matrix3 result = GetIdentity();
            result.Translate(x, y);

            return result;
        }

        public static Matrix3 GetScale(float x, float y)
        {
            Matrix3 result = GetIdentity();
            result.Scale(x, y);

            return result;
        }

    }
}
