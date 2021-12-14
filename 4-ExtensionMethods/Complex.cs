namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.Real = re;
            this.Imaginary = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real { get; }

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary { get; }

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus
        {
            get => Math.Sqrt(Real*Real + Imaginary*Imaginary);
        }

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase
        {
            get => Math.Atan2(Real, Imaginary);
        }

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            if (Imaginary == 0d)
            {
                return Real.ToString();
            }
            var imAbsoluteValue = Math.Abs(Imaginary);
            var imValue = imAbsoluteValue == 1.0 ? "" : imAbsoluteValue.ToString();
            string sign;
            if (Real == 0d)
            {
                sign = Imaginary > 0 ? "" : "-";
                return sign + "i" + imValue;
            }
            else
            {
                sign = Imaginary > 0 ? "+" : "-";
                return $"{Real} {sign} i{imValue}";
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType() || ReferenceEquals(null, obj))
            {
                return false;
            }
            else if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals(obj as IComplex);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }
    }
}
