namespace hw1
{
    using System;
    using static System.Math;

    internal struct Matrix2x2
    {
        public double M00 { get; }
        public double M01 { get; }

        public double M10 { get; }
        public double M11 { get; }

        public Matrix2x2(
            double m00, double m01,
            double m10, double m11)
        {
            M00 = m00;
            M01 = m01;

            M10 = m10;
            M11 = m11;
        }


        public static Matrix2x2 operator -(Matrix2x2 a) =>
            new Matrix2x2(
                -a.M00, -a.M01,
                -a.M10, -a.M11);

        public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(
                a.M00 + b.M00, a.M01 + b.M01,
                a.M10 + b.M10, a.M11 + b.M11);

        public static Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b) =>
            a + (-b);

        public static Matrix2x2 operator *(Matrix2x2 a, double scalar) =>
            new Matrix2x2(
                a.M00 * scalar, a.M01 * scalar,
                a.M10 * scalar, a.M11 * scalar);

        public static Matrix2x2 operator *(double scalar, Matrix2x2 a) =>
            a * scalar;

        public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(
                a.M00 * b.M00 + a.M01 * b.M10, a.M00 * b.M01 + a.M01 * b.M11,
                a.M10 * b.M00 + a.M11 * b.M10, a.M10 * b.M01 + a.M11 * b.M11);

        public static double MaximalNorm(Matrix2x2 a)
        {
            var maxInRow0 = Max(Abs(a.M00), Abs(a.M01));
            var maxInRow1 = Max(Abs(a.M10), Abs(a.M11));

            return Max(maxInRow0, maxInRow1);
        }

        public double MaximalNorm() =>
            MaximalNorm(this);

        #endregion

        #region Euclidean

        public static double EuclideanNorm(Matrix2x2 a)
        {
            var row0 = a.M00 * a.M00 + a.M01 * a.M01;
            var row1 = a.M10 * a.M10 + a.M11 * a.M11;

            return Sqrt(row0 + row1);
        }

        public double EuclideanNorm() =>
            EuclideanNorm(this);

        #endregion

        #endregion

        #region Utils

        #region Trace

        public static double Trace(Matrix2x2 a) =>
            a.M00 + a.M11;

        public double Trace() =>
            Trace(this);

        public static double Determinant(Matrix2x2 a) =>
            (a.M00 * a.M11) - (a.M10 * a.M01);

        public double Determinant() =>
            Determinant(this);

        public static Matrix2x2 Transpose(Matrix2x2 a) =>
            new Matrix2x2(
                a.M00, a.M10,
                a.M01, a.M11);

        public Matrix2x2 Transpose() =>
            Transpose(this);


        public static Matrix2x2 Inverse(Matrix2x2 a)
        {
            var determinant = Determinant(a);
            if (determinant == 0)
            {
                throw new Exception("There is no inverse matrix for this matrix");
            }

            return (new Matrix2x2(a.M11, a.M01, a.M10, a.M00)) * (1 / determinant);
        }

        public Matrix2x2 Inverse() =>
            Inverse(this);

        public static Matrix2x2 Symmetrize(Matrix2x2 a) =>
            0.5 * (a + a.Transpose());

        public Matrix2x2 Symmetrize() =>
            Symmetrize(this);

        public static Matrix2x2 Antisymmetrize(Matrix2x2 a)
        {
            Random random = new Random();
            double randomNumber = random.NextDouble() * random.Next(Int32.MinValue, Int32.MaxValue);

            return new Matrix2x2(
                a.M00, 2 * a.M01 - randomNumber,
                randomNumber, a.M11);
        }

        public Matrix2x2 AntiSymmetrize() =>
            Antisymmetrize(this);

        #endregion

        #endregion

        #region System

        public override string ToString() =>
            $"( {M00,5:0.00...}  {M01,5:0.00...} )\n( {M10,5:0.00...}  {M11,5:0.00...} )";

        #endregion
    }
}