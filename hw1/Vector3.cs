namespace hw1
{
    using static System.Math;

    internal struct Vector3
    {
        public double V0 { get; }
        public double V1 { get; }
        public double V2 { get; }

        public Vector3(double v0, double v1, double v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }

        #region Operators

        public static Vector3 operator -(Vector3 a) =>
            new Vector3(
                -a.V0,
                -a.V1,
                -a.V2);

        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new Vector3(
                a.V0 + b.V0,
                a.V1 + b.V1,
                a.V2 + b.V2);

        public static Vector3 operator -(Vector3 a, Vector3 b) =>
            a + (-b);

        public static Vector3 operator *(Vector3 a, double scalar) =>
            new Vector3(
                a.V0 * scalar,
                a.V1 * scalar,
                a.V2 * scalar);

        public static Vector3 operator *(double scalar, Vector3 a) =>
            a * scalar;

        #endregion

        #region Norms

        #region Absolute

        public static double AbsoluteNorm(Vector3 a) =>
            Abs(a.V0) + Abs(a.V1) + Abs(a.V2);

        public double AbsoluteNorm() =>
            AbsoluteNorm(this);

        #endregion

        #region Maximal

        public static double MaximalNorm(Vector3 a) =>
            Max(a.V0, Max(a.V1, a.V2));

        public double MaximalNorm() =>
            MaximalNorm(this);

        #endregion

        #region Euclidean

        public static double EuclideanNorm(Vector3 a)
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

        public static double ScalarProduct(Vector3 a, Vector3 b) =>
            a.V0 * b.V0 + a.V1 * b.V1 + a.V2 * b.V2;

        public double ScalarProduct(Vector3 anotherVector) =>
            ScalarProduct(this, anotherVector);

        #endregion

        #region Vector Product

        public static Vector3 VectorProduct(Vector3 a, Vector3 b) =>
            new Vector3(
                a.V1 * b.V2 - a.V2 * b.V1,
                a.V2 * b.V0 - a.V0 * b.V2,
                a.V0 * b.V1 - a.V1 * b.V0
            );

        public Vector3 VectorProduct(Vector3 anotherVector) =>
            VectorProduct(this, anotherVector);

        #endregion

        #region Wedge Product

        public static double WedgeProduct(Vector3 a, Vector3 b) =>
            (new Matrix3x3(
                b.V0, 1, a.V0,
                b.V1, 1, a.V1,
                b.V2, 1, a.V2)).Determinant();

        public double WedgeProduct(Vector3 a) =>
            WedgeProduct(this, a);

        public static double TripleWedgeProduct(Vector3 a, Vector3 b, Vector3 c) =>
        (new Matrix3x3(
            a.V0, b.V0, c.V0,
            a.V1, b.V1, c.V1,
            a.V2, b.V2, c.V2)).Determinant();

        public double TripleWedgeProduct(Vector3 a, Vector3 b) =>
            TripleWedgeProduct(this, a, b);

        #endregion

        #endregion

        #region Utils

        #region Transpose

        public static CoVector3 Transpose(Vector3 a) =>
            new CoVector3(a.V0, a.V1, a.V2);

        public CoVector3 Transpose() =>
            Transpose(this);

        #endregion

        #endregion

        #region System

        public override string ToString() =>
            $"( {V0,5:0.00...} )\n( {V1,5:0.00...} )\n( {V2,5:0.00...} )";

        #endregion
    }
}