namespace StaticMatrices
{
    using static System.Math;

    internal struct CoVector3
    {
        public double V0 { get; }
        public double V1 { get; }
        public double V2 { get; }

        public CoVector3(double v0, double v1, double v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }

        #region Operators

        public static CoVector3 operator -(CoVector3 a) =>
            new CoVector3(
                -a.V0,
                -a.V1,
                -a.V2);

        public static CoVector3 operator +(CoVector3 a, CoVector3 b) =>
            new CoVector3(
                a.V0 + b.V0,
                a.V1 + b.V1,
                a.V2 + b.V2);

        public static CoVector3 operator -(CoVector3 a, CoVector3 b) =>
            a + (-b);

        public static CoVector3 operator *(CoVector3 a, double scalar) =>
            new CoVector3(
                a.V0 * scalar,
                a.V1 * scalar,
                a.V2 * scalar);

        public static CoVector3 operator *(double scalar, CoVector3 a) =>
            a * scalar;

        #endregion

        #region Norms

        #region Absolute

        public static double AbsoluteNorm(CoVector3 a) =>
            Abs(a.V0) + Abs(a.V1) + Abs(a.V2);

        public double AbsoluteNorm() =>
            AbsoluteNorm(this);

        #endregion

        #region Maximal

        public static double MaximalNorm(CoVector3 aa) =>
            Max(aa.V0, Max(aa.V1, aa.V2));

        public double MaximalNorm() =>
            MaximalNorm(this);

        #endregion

        #region Euclidean

        public static double EuclideanNorm(CoVector3 a)
        {
            var norm0 = Abs(a.V0);
            var norm1 = Abs(a.V1);
            var norm2 = Abs(a.V2);

            return Sqrt(norm0 * norm0 + norm1 * norm1 + norm2 * norm2);
        }

        public double EuclideanNorm() =>
            EuclideanNorm(this);

        #endregion

        #endregion

        #region Vector Multiplication

        #region Scalar Product

        public static double ScalarProduct(CoVector3 a, CoVector3 b) =>
            a.V0 * b.V0 + a.V1 * b.V1 + a.V2 * b.V2;

        public double ScalarProduct(CoVector3 a) =>
            ScalarProduct(this, a);

        #endregion

        #region Vector Product

        public static CoVector3 VectorProduct(CoVector3 a, CoVector3 b) =>
            new CoVector3(
                a.V1 * b.V2 - a.V2 * b.V1,
                a.V2 * b.V0 - a.V0 * b.V2,
                a.V0 * b.V1 - a.V1 * b.V0
            );

        public CoVector3 VectorProduct(CoVector3 a) =>
            VectorProduct(this, a);

        #endregion

        #region Wedge Product

        public static double WedgeProduct(CoVector3 a, CoVector3 b) =>
            (new Matrix3x3(
                b.V0, 1, a.V0,
                b.V1, 1, a.V1,
                b.V2, 1, a.V2)).Determinant();

        public double WedgeProduct(CoVector3 a) =>
            WedgeProduct(this, a);

        public static double TripleWedgeProduct(CoVector3 a, CoVector3 b, CoVector3 c) =>
        (new Matrix3x3(
            a.V0, b.V0, c.V0,
            a.V1, b.V1, c.V1,
            a.V2, b.V2, c.V2)).Determinant();

        public double TripleWedgeProduct(CoVector3 a, CoVector3 b) =>
            TripleWedgeProduct(this, a, b);

        #endregion

        #endregion

        #region Utils

        #region Transpose

        public static Vector3 Transpose(CoVector3 a) =>
            new Vector3(a.V0, a.V1, a.V2);

        public Vector3 Transpose() =>
            Transpose(this);

        #endregion

        #endregion

        #region System

        public override string ToString() =>
            $"( {V0,5:0.00...}  {V1,5:0.00...}  {V2,5:0.00...} )";

        #endregion
    }
}