namespace School.DataAccess.Models;

/// <summary>
/// Model służący do zdefiniowania różnych operatorów potrzebnych przy mapowaniu wartości z parametrów zapytania
/// </summary>
public class OperatorModel
{
    public string FieldName { get; set; }
    public OperatorType Operator { get; set; }
    public string Value { get; set; }
}
