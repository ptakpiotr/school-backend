﻿using System.Text;

namespace School.DataAccess
{
    public static class Utils
    {
        public static string BuildCondition(params OperatorModel[] operators)
        {
            StringBuilder expr = new();

            for (int i = 0; i < operators.Length; i++)
            {
                if (i == operators.Length - 1)
                {
                    expr.Append($" {operators[i].FieldName} {GetOperator(operators[i])} '{operators[i].Value}'");
                }
                else
                {
                    expr.Append($" {operators[i].FieldName} {GetOperator(operators[i])} '{operators[i].Value}' AND");
                }
            }

            return expr.ToString();
        }

        private static string GetOperator(OperatorModel om) => om.Operator switch
        {
            OperatorType.GreaterThan => ">",
            OperatorType.LessThan => "<",
            OperatorType.Equal => "=",
            _ => "!="
        };
    }
}
