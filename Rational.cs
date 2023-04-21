using System;
using System.Linq;


public struct Rational
{
    public static readonly Rational Zero = new Rational(0, 1);
    public static readonly Rational One = new Rational(1, 1);

    private int numerator;
    private int denominator;

    public Rational(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException();

        this.numerator = numerator;
        this.denominator = denominator;
    }

    public static Rational operator +(Rational x, Rational y)
    {
        return new Rational(
            x.numerator * y.denominator + y.numerator * x.denominator,
            x.denominator * y.denominator
        );
    }

    public static Rational operator -(Rational x, Rational y)
    {
        return new Rational(
            x.numerator * y.denominator - y.numerator * x.denominator,
            x.denominator * y.denominator
        );
    }

    public static Rational operator *(Rational x, Rational y)
    {
        return new Rational(
            x.numerator * y.numerator,
            x.denominator * y.denominator
        );
    }

    public static Rational operator /(Rational x, Rational y)
    {
        return new Rational(
            x.numerator * y.denominator,
            x.denominator * y.numerator
        );
    }

    public override string ToString()
    {
        if (numerator == 0)
            return "0";

        if (denominator == 1)
            return numerator.ToString();

        return $"{numerator} / {denominator}";
    }
    private static Rational Simplify(Rational x)
    {
        int gcd = GetGreatestCommonDivisor(x.numerator, x.denominator);
        return new Rational(x.numerator / gcd, x.denominator / gcd);
    }
    private static int GetGreatestCommonDivisor(int a, int b)
    {
        while (b > 0)
        {
            int rem = a % b;
            a = b;
            b = rem;
        }
        return a;
    }
    public static Rational EvalExpression(Rational x, Rational y, string oper)
    {
        if (oper == "+")
            return Simplify(x + y);
        if (oper == "-")
            return Simplify(x - y);
        if (oper == "*")
            return Simplify(x * y);
        if (oper == ":")
            return Simplify(x / y);
        throw new Exception("Syntax Error!");
    }
}

