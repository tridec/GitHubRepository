Imports System.Net
Imports System.IO

Public Class clsDocumentFetch
    Inherits System.Web.UI.Page
    Private strDocName As String = ""
    Private strSourceURL As String = ""
    Private intACQRequestID As Integer = 0
    Private binDocumentBinary As Byte()
    Private intDocLen As Integer = 0
    Private strDocMimeType As String = ""
    Private bLogError As Boolean
    Public Property LogError()
        Get
            Return bLogError
        End Get
        Set(ByVal value)
            bLogError = value
        End Set
    End Property

    Public Property DocName()
        Get
            Return strDocName
        End Get
        Set(ByVal value)
            strDocName = value
        End Set
    End Property
    Public Property SourceURL()
        Get
            Return strSourceURL
        End Get
        Set(ByVal Value)
            strSourceURL = Value
        End Set
    End Property

    Public Property ACQRequestID()
        Get
            Return intACQRequestID
        End Get
        Set(ByVal Value)
            intACQRequestID = Value
        End Set
    End Property

    Public Property documentBinary()
        Get
            Return binDocumentBinary
        End Get
        Set(ByVal Value)
            binDocumentBinary = Value
        End Set
    End Property

    Public Property DocLen()
        Get
            Return intDocLen
        End Get
        Set(ByVal Value)
            intDocLen = Value
        End Set
    End Property
    Public Property DocMimeType()
        Get
            Return strDocMimeType
        End Get
        Set(ByVal Value)
            strDocMimeType = Value
        End Set
    End Property
    Function GetDocument() As Boolean
        Dim Req As HttpWebRequest
        Dim SourceStream As System.IO.Stream
        Dim Response As HttpWebResponse


        Try
            'create a web request to the URL   
            Req = HttpWebRequest.Create(strSourceURL)

            'get a response from web site   
            Response = Req.GetResponse()

            'Source stream with requested document   
            SourceStream = Response.GetResponseStream()
            strDocMimeType = Response.ContentType

            'SourceStream has no ReadAll, so we must read data block-by-block   
            'Temporary Buffer and block size   
            Dim Buffer(4096) As Byte, BlockSize As Integer

            'Memory stream to store data   
            Dim TempStream As New MemoryStream
            Do
                BlockSize = SourceStream.Read(Buffer, 0, 4096)
                If BlockSize > 0 Then TempStream.Write(Buffer, 0, BlockSize)
            Loop While BlockSize > 0

            'return the document binary data  
            binDocumentBinary = TempStream.ToArray()
            intDocLen = TempStream.Length
        Catch ex As Exception
            'if not from admin page save error log
            If bLogError Then
                Dim objError As New clsAcquisitionRequestError
                objError.AcqRequestID = intACQRequestID
                objError.ErrorDescription = "Unable to retrieve document name: """ + strDocName + """ from Source URL:""" + strSourceURL + """."
                objError.SystemErrorMessage = ex.Message.ToString
                objError.SaveError()
            End If

            Return False

        Finally
            Try
                SourceStream.Close()
                Response.Close()
            Catch
            End Try
        End Try
        Return True
    End Function
End Class
