namespace hw1
{
    using System;
    using static System.Math;

    internal struct Matrix3x3
    {
        public double M00 { get; }
        public double M01 { get; }
        public double M02 { get; }

        public double M10 { get; }
        public double M11 { get; }
        public double M12 { get; }

        public double M20 { get; }
        public double M21 { get; }
        public double M22 { get; }

        public Matrix3x3(
            double m00, double m01, double m02,
            double m10, double m11, double m12,
            double m20, double m21, double m22)
        {
            M00 = m00;
            M01 = m01;
            M02 = m02;

            M10 = m10;
            M11 = m11;
            M12 = m12;

            M20 = m20;
            M21 = m21;
            M22 = m22;
        }

        #region Operators

        public static Matrix3x3 operator -(Matrix3x3 a) =>
            new Matrix3x3(
                -a.M00, -a.M01, -a.M02,
                -a.M10, -a.M11, -a.M12,
                -a.M20, -a.M21, -a.M22);

        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b) =>
            new Matrix3x3(
                a.M00 + b.M00, a.M01 + b.M01, a.M02 + b.M02,
                a.M10 + b.M10, a.M11 + b.M11, a.M12 + b.M12,
                a.M20 + b.M20, a.M21 + b.M21, a.M22 + b.M22);

        public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b) =>
            a + (-b);

        public static Matrix3x3 operator *(Matrix3x3 a, double scalar) =>
            new Matrix3x3(
                a.M00 * scalar, a.M01 * scalar, a.M02 * scalar,
                a.M10 * scalar, a.M11 * scalar, a.M12 * scalar,
                a.M20 * scalar, a.M21 * scalar, a.M22 * scalar);

        public static Matrix3x3 operator *(double scalar, Matrix3x3 a) =>
            a * scalar;

        public static CoVector3 operator *(CoVector3 a, Matrix3x3 b) =>
            new CoVector3(
                b.M00 * a.V0 + b.M10 * a.V1 + b.M20 * a.V2,
                b.M01 * a.V0 + b.M11 * a.V1 + b.M21 * a.V2,
                b.M02 * a.V0 + b.M12 * a.V1 + b.M22 * a.V2);

        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b) =>
            new Matrix3x3(
                a.M00 * b.M00 + a.M01 * b.M10 + a.M02 * b.M20, a.M00 * b.M01 + a.M01 * b.M11 + a.M02 * b.M21, a.M00 * b.M02 + a.M01 * b.M12 + a.M02 * b.M22,
                a.M10 * b.M00 + a.M11 * b.M10 + a.M12 * b.M20, a.M10 * b.M01 + a.M11 * b.M11 + a.M12 * b.M21, a.M10 * b.M02 + a.M11 * b.M12 + a.M12 * b.M22,
                a.M20 * b.M00 + a.M21 * b.M10 + a.M22 * b.M20, a.M20 * b.M01 + a.M21 * b.M11 + a.M22 * b.M21, a.M20 * b.M02 + a.M21 * b.M12 + a.M22 * b.M22);

        #endregion

        #region Norms

        #region Maximal

        public static double MaximalNorm(Matrix3x3 a)
        {
            var maxInRow0 = Max(Abs(a.M00), Max(Abs(a.M01), Abs(a.M02)));
            var maxInRow1 = Max(Abs(a.M10), Max(Abs(a.M11), Abs(a.M12)));
            var maxInRow2 = Max(Abs(a.M20), Max(Abs(a.M21), Abs(a.M22)));

            return Max(maxInRow0, Max(maxInRow1, maxInRow2));
        }

        public double MaximalNorm() =>
            MaximalNorm(this);

        #endregion

        #region Euclidean

        public static double EuclideanNorm(Matrix3x3 a)
        {
            var row0 = a.M00 * a.M00 + a.M01 * a.M01 + a.M02 * a.M02;
            var row1 = a.M10 * a.M10 + a.M11 * a.M11 + a.M12 * a.M12;
            var row2 = a.M20 * a.M20 + a.M21 * a.M21 + a.M22 * a.M22;

            return Sqrt(row0 + row1 + row2);
        }

        public double EuclideanNorm() =>
            EuclideanNorm(this);

        #endregion

        #endregion

        #region Utils

        #region Trace

        public static double Trace(Matrix3x3 a) =>
            a.M00 + a.M11 + a.M22;

        public double Trace() =>
            Trace(this);

        #endregion

        #region Determinant

        public static double Determinant(Matrix3x3 a) =>
            (a.M00 * a.M11 * a.M22 + a.M01 * a.M12 * a.M20 + a.M10 * a.M21 * a.M02) -
            (a.M20 * a.M11 * a.M02 + a.M21 * a.M12 * a.M00 + a.M10 * a.M01 * a.M22);

        public double Determinant() =>
            Determinant(this);

        #endregion

        #region Transpose

        public static Matrix3x3 Transpose(Matrix3x3 a) =>
            new Matrix3x3(
                a.M00, a.M10, a.M20,
                a.M01, a.M11, a.M21,
                a.M02, a.M12, a.M22);

        public Matrix3x3 Transpose() =>
            Transpose(this);

        #endregion

        #region Inverse

        public static Matrix3x3 Inverse(Matrix3x3 a)
        {
            var determinant = Determinant(a);
            if (determinant == 0)
            {
                throw new Exception("There is no inverse matrix for this matrix");
            }

            var minor00 = (new Matrix2x2(a.M11, a.M12, a.M21, a.M22)).Determinant();
            var minor01 = (new Matrix2x2(a.M10, a.M12, a.M20, a.M22)).Determinant();
            var minor02 = (new Matrix2x2(a.M10, a.M11, a.M20, a.M21)).Determinant();

            var minor10 = (new Matrix2x2(a.M01, a.M02, a.M21, a.M22)).Determinant();
            var minor11 = (new Matrix2x2(a.M00, a.M02, a.M20, a.M22)).Determinant();
            var minor12 = (new Matrix2x2(a.M00, a.M01, a.M20, a.M21)).Determinant();

            var minor20 = (new Matrix2x2(a.M01, a.M02, a.M11, a.M12)).Determinant();
            var minor21 = (new Matrix2x2(a.M00, a.M02, a.M10, a.M12)).Determinant();
            var minor22 = (new Matrix2x2(a.M00, a.M01, a.M10, a.M11)).Determinant();

            var transposedMinorMatrix = (new Matrix3x3(
                minor00, minor01, minor02,
                minor10, minor11, minor12,
                minor20, minor21, minor22)).Transpose();

            return transposedMinorMatrix * (1 / determinant);
        }

        public Matrix3x3 Inverse() =>
            Inverse(this);

        #endregion

        #region Symmetrize

        public static Matrix3x3 Symmetrize(Matrix3x3 a) =>
            0.5 * (a + a.Transpose());

        public Matrix3x3 Symmetrize() =>
            Symmetrize(this);

        #endregion

        #region Antisymmetrize

        public static Matrix3x3 Antisymmetrize(Matrix3x3 a)
        {
            return new Matrix3x3(
                0, a.M01 - a.M10, a.M02 - a.M20,
                a.M10 - a.M01, 0, a.M12 - a.M21,
                a.M20 - a.M02, a.M21 - a.M12, 0) * 0.5;
        }

        public Matrix3x3 AntiSymmetrize() =>
            Antisymmetrize(this);

        #endregion

        #endregion

        #region System

        public override string ToString() =>
            $"( {M00,5:0.00...}  {M01,5:0.00...}  {M02,5:0.00...} )\n( {M10,5:0.00...}  {M11,5:0.00...}  {M12,5:0.00...} )\n( {M20,5:0.00...}  {M21,5:0.00...}  {M22,5:0.00...} )";

        #endregion
    }
}