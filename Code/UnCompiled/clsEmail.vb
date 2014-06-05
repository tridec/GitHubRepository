'Add the namespace for the email-related classes AND System.Net for the NetworkCredential class
Imports System.Net.Mail
Imports System.Net
Imports Telerik.Web.UI
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Net.Configuration

Public Class clsEmail
    Inherits System.Web.UI.Page
    Private strFrom As String = ""
    Private strTo As String = ""
    Private strCC As String = ""
    Private strBCC As String = ""
    Private strSubject As String = ""
    Private strBody As String = ""
    Private bHTML As Boolean = False
    Private intRecordId As Integer = 0
    Private intRecordTypeID As Integer = 0


    Public Property EmailFrom()
        Get
            Return strFrom
        End Get
        Set(ByVal Value)
            strFrom = Value
        End Set
    End Property
    Public Property EmailTo()
        Get
            Return strTo
        End Get
        Set(ByVal Value)
            strTo = Value
        End Set
    End Property
    Public Property EmailCC()
        Get
            Return strCC
        End Get
        Set(ByVal Value)
            strCC = Value
        End Set
    End Property
    Public Property EmailBCC()
        Get
            Return strBCC
        End Get
        Set(ByVal Value)
            strBCC = Value
        End Set
    End Property
    Public Property EmailSubject()
        Get
            Return strSubject
        End Get
        Set(ByVal Value)
            strSubject = Value
        End Set
    End Property
    Public Property EmailBody()
        Get
            Return strBody
        End Get
        Set(ByVal Value)
            strBody = Value
        End Set
    End Property
    Public Property EmailHTML()
        Get
            Return bHTML
        End Get
        Set(ByVal Value)
            bHTML = Value
        End Set
    End Property

    Public Property RecordId()
        Get
            Return intRecordId
        End Get
        Set(ByVal Value)
            intRecordId = Value
        End Set
    End Property

    Public Property RecordTypeId()
        Get
            Return intRecordTypeID
        End Get
        Set(ByVal Value)
            intRecordTypeID = Value
        End Set
    End Property

    Public Enum RecordType

        User = 1
        ProjectTaskDeliverable = 2
        Project = 3

    End Enum

    ' scans the lenght of the html email and adds CRLF so there are no problems with html lenght
    Private Function BreakLongHTML(ByVal strText As String, ByVal iWidth As Integer) As String
        If strText.Length <= iWidth Then Return strText
        Dim sResult As String = strText
        Dim sChar As String
        Dim iEn As Long
        Dim iLineNO As Long = iWidth
        Do While sResult.Length > iLineNO
            For iEn = iLineNO To 1 Step -1
                sChar = sResult.Chars(iEn)
                If sChar = " " Then
                    sResult = sResult.Remove(iEn, 1)
                    sResult = sResult.Insert(iEn, vbCrLf)
                    iLineNO += iWidth
                    Exit For
                End If
            Next
        Loop
        Return sResult
    End Function

    Function SendEmail() As String
        '!!! UPDATE THIS VALUE TO YOUR EMAIL ADDRESS
        'Const ToAddress As String = "scott.rodabaugh@tridectech.com"
        LogEmail()
        Try
            '(1) Create the MailMessage instance
            Dim objMailMessage As New MailMessage()
            'objMailMessage.From = New System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings("From"))

            '(2) Assign the MailMessage's properties
            'Dim MailFrom As MailAddress = ToAddress.
            If EmailFrom.Length > 0 Then
                Dim maFromAddress As MailAddress = New MailAddress(EmailFrom)
                objMailMessage.From = maFromAddress
            End If
            'multiple strTo & strCC emails must be separated by comma
            objMailMessage.To.Add(strTo)
            If strCC <> "" Then
                'check for CC's to avoid exception when strCC is empty string.
                objMailMessage.CC.Add(strCC)
            End If
            'objMailMessage.Bcc.Add(strBCC)
            objMailMessage.Subject = strSubject
            If bHTML Then
                objMailMessage.Body = BreakLongHTML(strBody, 50)
            Else
                objMailMessage.Body = strBody
            End If

            objMailMessage.IsBodyHtml = bHTML

            '(3) Create the SmtpClient object
            Dim smtp As New SmtpClient

            'If Session("SMTPServer") <> "" Then
            'smtp.Host = "localhost"
            'End If

            ''Set the SMTP settings...
            'smtp.Host = Hostname.Text
            'If Not String.IsNullOrEmpty(Port.Text) Then
            'smtp.Port = 25
            'End If

            'If Not String.IsNullOrEmpty(Username.Text) Then
            'smtp.Credentials = New NetworkCredential("pfefferlework@att.net", "work1234")
            'End If

            '(4) Send the MailMessage (will use the Web.config settings)
            'smtp.Port = System.Configuration.ConfigurationManager.AppSettings("Port")
            'smtp.Host = System.Configuration.ConfigurationManager.AppSettings("Host")
            smtp.Send(objMailMessage)

            'Display a client-side popup, explaining that the email has been sent
            'ScriptManager.RegisterStartupScript(Me,Me.GetType(), "HiMom!", String.Format("alert('An test email has successfully been sent to {0}');", strTo.Replace("'", "\'")), True)
            Return ("Email Sent")
        Catch smtpEx As SmtpException
            'A problem occurred when sending the email message
            Return ("There was a problem in sending the email:" + smtpEx.Message.Replace("'", "\'"))

        Catch generalEx As Exception
            'Some other problem occurred
            Return ("There was a general problem:" + generalEx.Message.Replace("'", "\'"))

        End Try
    End Function


    '  ' log email in db
    Private Sub LogEmail()

        ' save the info to the database insert or update
        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "SentEmailLogInsert"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
        Dim m As MembershipUser = Membership.GetUser()
        If Not m Is Nothing Then
            db.AddInParameter(objCommand, "@UserId", DbType.String, m.ProviderUserKey.ToString())
        End If
        If EmailFrom.Length > 0 Then
            db.AddInParameter(objCommand, "@EmailFrom", DbType.String, EmailFrom)
        Else
            Dim configurationFile As System.Configuration.Configuration = Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath)
            Dim mailSettings As MailSettingsSectionGroup = TryCast(configurationFile.GetSectionGroup("system.net/mailSettings"), MailSettingsSectionGroup)
            Dim smtpSection As SmtpSection = mailSettings.Smtp
            'read the from email of the web.config file  
            Dim fromEmail As String = smtpSection.From
            db.AddInParameter(objCommand, "@EmailFrom", DbType.String, fromEmail)
        End If


        db.AddInParameter(objCommand, "@EmailTo", DbType.String, EmailTo)
        db.AddInParameter(objCommand, "@EmailCC", DbType.String, EmailCC)
        db.AddInParameter(objCommand, "@EmailBCC", DbType.String, EmailBCC)
        db.AddInParameter(objCommand, "@EmailSubject", DbType.String, EmailSubject)
        db.AddInParameter(objCommand, "@EmailBody", DbType.String, EmailBody)
        db.AddInParameter(objCommand, "@RecordId", DbType.Int32, RecordId)
        db.AddInParameter(objCommand, "@RecordTypeId", DbType.Int32, RecordTypeId)
        db.ExecuteNonQuery(objCommand)
    End Sub

End Class

