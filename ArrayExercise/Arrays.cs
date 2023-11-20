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
    
    float[] array1;

    [Header("Vectors")]
    [SerializeField]
    int dimensionVector = 3; //Si nos son de la misma dimension no va a ser posible la suma.
    [SerializeField]
    float[] vector1;
    [SerializeField]
    float[] vector2;

    [Header("Matrix")]
    [SerializeField]
    int columns = 3;
    [SerializeField]
    int rows = 3;
    float[,] matrix1;
    float[,] matrix2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("El valor de la sucesi�n de Fibonacci en la posici�n "+ position+ " es "+ Fibonacci(position));
       
        array1 = new float[size];
        FillRandomArray(array1);
        Debug.Log("Array1 es: ");
        PrintArray(array1);
        //GetNegative
        Debug.Log("Los numeros negativos del array son: ");
        PrintArray(GetNegativesNumbers(array1));

        //Deben ser de la misma dimension
        vector1 = new float[dimensionVector];
        vector2 = new float[dimensionVector];
        //Rellenamos los vectores
        FillRandomArray(vector1); PrintArray(vector1);
        FillRandomArray(vector2); PrintArray(vector2);
        //Sumamos y mostramos por consola
        Debug.Log("Suma: ");
        PrintArray(AddVector(vector1, vector2));
        Debug.Log("Resta: ");
        PrintArray(SubstractVector(vector1, vector2));
        Debug.Log("Multiplicacion por un escalar: ");
        PrintArray(ScalarMultiplicationVector(vector1, 0.5f));
        Debug.Log("Producto escalar: "+ DotVector(vector1, vector2));

        Debug.Log("Magnitud: " + MagnitudeVector(vector1));
        Debug.Log("Distancia: " + DistanceVector(vector1,vector2));
        Debug.Log("Angulo: " + AngleBetweenVectors(vector1,vector2));


        //Ejercicios de Matrix
        matrix1 = new float[rows,columns];
        matrix2 = new float[rows,columns];

        FillRandomMatrix(matrix1); PrintMatrix(matrix1);
        FillRandomMatrix(matrix2); PrintMatrix(matrix2);

        Debug.Log("Suma de vectores");
        PrintMatrix(AddMatrix(matrix1,matrix2));


    }
    void FillRandomMatrix(float[,] matrixRef)
    {
        for (int i = 0; i < matrixRef.GetLength(0); i++)
        {
            for(int j = 0; j < matrixRef.GetLength(1); j++)
                matrixRef[i,j] = Random.Range(-100, 100);

        }

    }

    void PrintMatrix(float[,] matrixRef) 
    {
        string result = "";
        for (int i = 0; i < matrixRef.GetLength(0); i++)
        {
            for (int j = 0; j < matrixRef.GetLength(1); j++)
                result += matrixRef[i, j] + "\t";

            result += "\n";//Para darle formato
        }

        Debug.Log(result);
    }

    float[,] AddMatrix(float[,] a, float[,] b) 
    {
        float[,] result = new float[a.GetLength(0),a.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
                result[i, j] = a[i, j] + b[i, j];
        }
        return result;
    }

    float[] AddVector(float[]a, float[] b) 
    {
        float[] result = new float[a.Length];
        for(int i = 0; i < a.Length; ++i) 
        {
            result[i] = a[i] +b[i];
        }

        return result;
    }

    float[] SubstractVector(float[] a, float[] b)
    {
        float[] result = new float[a.Length];
        for (int i = 0; i < a.Length; ++i)
        {
            result[i] = a[i] - b[i];
        }

        return result;
    }
    float[] ScalarMultiplicationVector(float[] numbers, float scalar)
    {
        float[] result = new float[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            result[i] = numbers[i] * scalar;
        }

        return result;

        //Si quisiese cambiar el array directamente y no devolver una copia. Podemos ahorrarnos el return y ser void
        //for (int i = 0; i < numbers.Length; i++)        
        //    numbers[i] *= scalar;


    }

    float DotVector(float[] a, float[] b)
    {
        float result = 0;
        for (int i = 0; i < a.Length; ++i)
        {
            result += a[i] * b[i];
        }
        return result;
    }

    float MagnitudeVector(float[] numbers)
    {
        float result = 0;
        for (int i = 0; i < numbers.Length; ++i)
        {
            result += Mathf.Pow(numbers[i], 2);//numbers[i]*numbers[i]
        }


        return Mathf.Sqrt(result);
    }

    float DistanceVector(float[] origin, float[] end)
    {
        float sum = 0;
        for (int i = 0; i < origin.Length; ++i)
        {
            sum += Mathf.Pow(end[i] - origin[i], 2);//(end[i] - origin[i])^2
        }
        return Mathf.Sqrt(sum);
    }

    float AngleBetweenVectors(float[] origin, float[] end)
    {
        float result = Mathf.Acos(DotVector(origin, end) /
                            (MagnitudeVector(origin) * MagnitudeVector(end)));
        return result;
    }


    void FillRandomArray(float[] arrayRef) 
    {
        for (int i = 0; i < arrayRef.Length; i++)
        {
            arrayRef[i] = Random.Range(-100, 100);          

        }

    }

    float[] GetNegativesNumbers(float[] numbers) 
    {
        int newLength = 0;
        //Calculamos el numero de valores negativos
        for (int i = 0; i < numbers.Length; ++i)
        {
            if (numbers[i] < 0)
                newLength++;
        }

        //Creamos el nuevo array resultado
        float[] result = new float[newLength];
        int index = 0;
        //Rellenamos el nuevo array con los resultados
        for(int i = 0; i < numbers.Length; ++i) 
        {
            if(numbers[i] < 0) 
            {
                result[index] = numbers[i];
                index++;//Para avanzar a la siguiente posici�n
            }
        }

        //Esto lo har�a m�s sencillo con una List<int> ya que la capacidad es din�mica y as� no recorro 2 veces el array
        return result;
    }

    void PrintArray(float[] numbers) 
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
