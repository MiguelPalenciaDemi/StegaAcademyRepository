using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    [Header("Fibonacci")]
    [SerializeField]    
    int position = 0;

    [Header("Arrays")]
    [SerializeField]
    int size = 0;
    
    int[] array1;

    [Header("Vectors")]
    [SerializeField]
    int dimensionVector = 3; //Si nos son de la misma dimension no va a ser posible la suma.
    [SerializeField]
    int[] vector1;
    [SerializeField]
    int[] vector2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("El valor de la sucesión de Fibonacci en la posición "+ position+ " es "+ Fibonacci(position));
       
        array1 = new int[size];
        FillRandomArray(array1);
        PrintArray(array1);
        //GetNegative
        PrintArray(GetNegativesNumbers(array1));

        //Deben ser de la misma dimension
        vector1 = new int[dimensionVector];
        vector2 = new int[dimensionVector];
        //Rellenamos los vectores
        FillRandomArray(vector1); PrintArray(vector1);
        FillRandomArray(vector2); PrintArray(vector2);
        //Sumamos y mostramos por consola
        PrintArray(AddVector(vector1, vector2));




    }

    int[] AddVector(int[]a, int[] b) 
    {
        int[] result = new int[a.Length];
        for(int i = 0; i < a.Length; ++i) 
        {
            result[i] = a[i] +b[i];
        }

        return result;
    }

    int[] SubstractVector(int[] a, int[] b)
    {
        int[] result = new int[a.Length];
        for (int i = 0; i < a.Length; ++i)
        {
            result[i] = a[i] - b[i];
        }

        return result;
    }

    void FillRandomArray(int[] arrayRef) 
    {
        for (int i = 0; i < arrayRef.Length; i++)
        {
            arrayRef[i] = Random.Range(-100, 100);          

        }

    }

    int[] GetNegativesNumbers(int[] numbers) 
    {
        int newLength = 0;
        //Calculamos el numero de valores negativos
        for (int i = 0; i < numbers.Length; ++i)
        {
            if (numbers[i] < 0)
                newLength++;
        }

        //Creamos el nuevo array resultado
        int[] result = new int[newLength];
        int index = 0;
        //Rellenamos el nuevo array con los resultados
        for(int i = 0; i < numbers.Length; ++i) 
        {
            if(numbers[i] < 0) 
            {
                result[index] = numbers[i];
                index++;//Para avanzar a la siguiente posición
            }
        }

        //Esto lo haría más sencillo con una List<int> ya que la capacidad es dinámica y así no recorro 2 veces el array
        return result;
    }

    void PrintArray(int[] numbers) 
    {
        string arrayString = " ";        
        for (int i = 0; i < numbers.Length; i++)
        {

            arrayString += numbers[i] + " ";

        }

        Debug.Log(arrayString);
    }
    int Fibonacci(int pos) 
    {
        int num1 = 0,
            num2 = 1;
        int sum  = 0;
        if(position > 0) 
        {
            sum = num2;//En caso de que sea posicion 1 no entraria en el bucle, por eso le damos un valor
            for(int i = 1; i<pos;i++) 
            {
                
                sum = num1 + num2;
                num1 = num2;
                num2 = sum;

            }
            return sum;
        }

        Debug.LogWarning("position not correct, it must be bigger than 1");
        return -1;
    }
}
