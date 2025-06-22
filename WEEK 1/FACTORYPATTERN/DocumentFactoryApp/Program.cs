// See https://aka.ms/new-console-template for more information
using System;

#region Product Interface
interface IDocument
{
    void Open();
}
#endregion

#region Concrete Products
class WordDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening a Word document.");
    }
}

class PdfDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening a PDF document.");
    }
}

class ExcelDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening an Excel document.");
    }
}
#endregion

#region Factory Base
abstract class DocumentFactory
{
    public abstract IDocument CreateDocument();
}
#endregion

#region Concrete Factories
class WordFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

class PdfFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new PdfDocument();
    }
}

class ExcelFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new ExcelDocument();
    }
}
#endregion

#region Main
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter document type to open (Word / PDF / Excel): ");
        string input = Console.ReadLine().Trim().ToLower();

        DocumentFactory factory = null;

        switch (input)
        {
            case "word":
                factory = new WordFactory();
                break;
            case "pdf":
                factory = new PdfFactory();
                break;
            case "excel":
                factory = new ExcelFactory();
                break;
            default:
                Console.WriteLine("Invalid document type entered.");
                return;
        }

        IDocument document = factory.CreateDocument();
        document.Open();
    }
}
#endregion
