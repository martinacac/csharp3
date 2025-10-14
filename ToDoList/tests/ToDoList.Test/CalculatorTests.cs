namespace ToDoList.Test;

public class CalculatorTest
{
    [Fact]
    public void Calculator_Add_ShouldReturnCorrectResult()
    {
        //arrange
        var calculator = new Calculator();
        //act
        var result = calculator.Add(2, 3);
        //assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Calculator_Divide_ThrowsDivisionByZeroException()
    {
        //arrange
        var calculator = new Calculator();
        //act + assert
        //Assert.Throws<> //chybí dopsat

    }
}

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        return a / b;
    }
}
