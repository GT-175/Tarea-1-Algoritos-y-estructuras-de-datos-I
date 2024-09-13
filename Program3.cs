using System;

public class Nodo(int valor)
{
    public int Valor { get; set; } = valor;
    public Nodo? Siguiente { get; set; } = null;
    public Nodo? Anterior { get; set; } = null;
}

public class ListaDobleEnlazada
{
    private Nodo? cabeza;
    private Nodo? cola;

    public ListaDobleEnlazada()
    {
        cabeza = null;
        cola = null;
    }

    public void InsertarAlInicio(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            nuevoNodo.Siguiente = cabeza;
            cabeza.Anterior = nuevoNodo;
            cabeza = nuevoNodo;
        }
    }

    public void InsertarAlFinal(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        if (cola == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            cola.Siguiente = nuevoNodo;
            nuevoNodo.Anterior = cola;
            cola = nuevoNodo;
        }
    }

    public void ImprimirLista()
    {

        Nodo actual = cabeza;

        while (actual != null)
        {
            Console.Write(actual.Valor + " ");

            actual = actual.Siguiente;

        }
        Console.WriteLine();
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Método para combinar las dos listas
    public static ListaDobleEnlazada MergeSorted(ListaDobleEnlazada lista1, ListaDobleEnlazada lista2)
    {
        ListaDobleEnlazada listaCombinada = new ListaDobleEnlazada();
        
        Console.WriteLine("Escriba e tipo de ordemaniento a realizar: Asc (Asendente) o Desc (Desendente)");

        string SortType = Console.ReadLine();


        if (SortType == "Asc")
        {

            Nodo actual1 = lista1.cola;


            Nodo actual2 = lista2.cola;


            while (actual1 != null && actual2 != null)
            {
                if (actual2.Valor <= actual1.Valor)
                {
                    listaCombinada.InsertarAlInicio(actual1.Valor);

                    actual1 = actual1.Anterior;

                }

                else if (actual2.Valor >= actual1.Valor)
                {
                    listaCombinada.InsertarAlInicio(actual2.Valor);

                    actual2 = actual2.Anterior;

                }
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            listaCombinada.InsertarAlInicio(actual2.Valor);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return listaCombinada;
        }

        else if (SortType == "Desc")
        {

            Nodo actual1 = lista1.cola;


            Nodo actual2 = lista2.cola;

            

            while (actual1 != null && actual2 != null)
            {
                if (actual1.Valor >= actual2.Valor)
                {
                    listaCombinada.InsertarAlFinal(actual1.Valor);

                    actual1 = actual1.Anterior;

                }

                else if (actual1.Valor <= actual2.Valor)
                {
                    listaCombinada.InsertarAlFinal(actual2.Valor);

                    actual2 = actual2.Anterior;

                }
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            listaCombinada.InsertarAlFinal(actual2.Valor);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return listaCombinada;
        }

        else
        {
            throw new InvalidOperationException("Solo se puede escribir 'Asc' o 'Desc'. Intente nuevamente");
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Método para invertir los valores de la lista
    public void Invert()
    {

        Nodo actual = cola;

        while (actual != null)
        {
            Console.Write(actual.Valor + " ");

            actual = actual.Anterior;

        }
        Console.WriteLine();
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Método para obtener el valor del medio
    public void GetMiddle()
    {
        int contador = 0;
        int mitad;

        Nodo actual = cola;
        Nodo NewActual = cola;

        while (actual != null)
        {
            contador += 1;

            actual = actual.Anterior;


        }

        if (contador % 2 == 0)
        {
            mitad = contador/2;
        }
        else
        {
        contador += 1;
            mitad = contador/2;
        }
        

        while (NewActual != null)
        {
            contador -= 1;
            if (contador == mitad)
            {
                Console.Write(NewActual.Valor + " ");
                break;
            }

            NewActual = NewActual.Anterior;

            
        }

        Console.WriteLine();
    }


}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
class Programa
{
    static void Main()
    {
        ListaDobleEnlazada lista1 = new ListaDobleEnlazada();
        lista1.InsertarAlFinal(1);
        lista1.InsertarAlFinal(2);
        lista1.InsertarAlFinal(4);
        lista1.InsertarAlFinal(6);

        ListaDobleEnlazada lista2 = new ListaDobleEnlazada();
        lista2.InsertarAlFinal(0);
        lista2.InsertarAlFinal(3);
        lista2.InsertarAlFinal(5);
        lista2.InsertarAlFinal(8);
        lista2.InsertarAlFinal(10);
        lista2.InsertarAlFinal(12);
        lista2.InsertarAlFinal(13);

        Console.WriteLine("Lista 1:");
        lista1.ImprimirLista();
        Console.WriteLine("Lista 2:");
        lista2.ImprimirLista();

        //Para ordenar las listas en orden ascendente o descendente
        ListaDobleEnlazada listaCombinada = ListaDobleEnlazada.MergeSorted(lista1, lista2);
        Console.WriteLine("Lista combinada:");
        listaCombinada.ImprimirLista();

        //Para invertir la lista seleccionada
        Console.WriteLine("Lista original");
        lista1.ImprimirLista();
        Console.WriteLine("Lista invertida");
        lista1.Invert();

        //Para Obtener el elemento del medio de la lista seleccionada
        Console.WriteLine("Elemento del medio de la lista seleccionada:");
        lista2.GetMiddle();
    }
}