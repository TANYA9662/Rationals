using System;

public class Parser
{
    private const string AllowedOperators = "+-:*";

    private static string[] ParseOperator(string expression)
    {
        string oper = "";
        string[] operands;
        string trimmed_expression = String.Concat(expression.Where(c => !Char.IsWhiteSpace(c)));
        for (int i = 0; i < AllowedOperators.Length; i++)
        {
            if (trimmed_expression.Contains(AllowedOperators[i]))
            {
                oper = AllowedOperators[i].ToString();
                break;
            }
        }
        if (oper.Length > 0)
        {
            operands = trimmed_expression.Split(oper);
            if (operands.Length == 2)
            {
                string[] opnd_op_opnd = { operands[0], oper, operands[1] };
                return opnd_op_opnd;
            }
                        
            throw new Exception("Syntax Error!");
        }
        else
        {
            string[] opnd_op_opnd = { trimmed_expression, "+", "0" };
            return opnd_op_opnd;
        }
    }
    
    private static Rational DetermineOperandType(string operand)
    {
        if (operand.Contains('/'))
        {
            var fraction_parts = operand.Split('/');
            if (fraction_parts.Length != 2)
            {
                throw new Exception("Syntax Error!");
            }
            else
            {
                try
                {
                    int num = Int32.Parse(fraction_parts[0]);
                    int den = Int32.Parse(fraction_parts[1]);
                    return new Rational(num, den);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        else if (operand.Contains('.'))
        {
            var split_str = operand.Split('.');
            var int_part = split_str[0];
            var decimal_part = split_str[1];
            var numerator = Int32.Parse(int_part + decimal_part);
            var denominator = (int)Math.Pow(10, decimal_part.Length);
            return new Rational(numerator, denominator);
        }
        else
        {
            try
            {
                int integ = Int32.Parse(operand);
                return new Rational(integ, 1);
            }
            catch
            {
                throw new Exception("Syntax Error!");
            }
        }
    }
    
    public static Rational Calculate(string user_input)
    {
        var parsed_user_input = Parser.ParseOperator(user_input);
        var first_operand = parsed_user_input[0];
        var second_operand = parsed_user_input[2];
        var parsed_operator = parsed_user_input[1];
        var ration_first_operand = Parser.DetermineOperandType(first_operand);
        var ration_second_operand = Parser.DetermineOperandType(second_operand);
        return Rational.EvalExpression(ration_first_operand, ration_second_operand, parsed_operator);
    }
}

