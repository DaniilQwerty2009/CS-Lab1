namespace Interfaces
{
    // Проверяет валидность введенных значений: только символы букв для строк, только натуральные числа для чисел
    public interface IValidate 
    {
        bool StringValidate(string value);
        bool NumberValidate(long value);
    }

    public interface IFirstLetterUpper
    {
        void FirstLetterUpper();
    }

}
