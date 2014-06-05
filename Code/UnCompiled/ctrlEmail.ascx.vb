Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.IO

Partial Public Class ctrlEmail
    Inherits System.Web.UI.UserControl
    Dim objEmail As New clsEmail
    Dim blnVisible As Boolean
    Dim strReturn As String = ""
    Dim bProjectQandA As Boolean = False
    Dim bNewProject As Boolean = False
    Public Property QADate() As String
        Get
            Return lblQADate.Text
        End Get
        Set(ByVal value As String)
            lblQADate.Text = value
        End Set
    End Property

    Public Property IDIQName() As String
        Get
            Return lblIDIQName.Text
        End Get
        Set(ByVal value As String)
            lblIDIQName.Text = value
        End Set
    End Property
    Public Property ProjectNumber() As String
        Get
            Return lblProjectNumber.Text
        End Get
        Set(ByVal value As String)
            lblProjectNumber.Text = value
        End Set
    End Property
    Public Property ProjectTitle() As String
        Get
            Return lblProjectTItle.Text
        End Get
        Set(ByVal value As String)
            lblProjectTItle.Text = value
        End Set
    End Property
    Public Property EmailModuleType() As String
        Get
            Return lblEmailModuleType.Text
        End Get
        Set(ByVal value As String)
            lblEmailModuleType.Text = value
        End Set
    End Property

    Public Property EmailTo() As String
        Get
            Return lblEmailTo.Text
        End Get
        Set(ByVal value As String)
            lblEmailTo.Text = value
        End Set
    End Property
    Public Property ProjectVendorID() As String
        Get
            Return lblProjectVendorID.Text
        End Get
        Set(ByVal value As String)
            lblProjectVendorID.Text = value
        End Set
    End Property

    Public Property EmailRecordID() As Int32
        Get
            Return lblEmailRecordID.Text
        End Get
        Set(ByVal value As Int32)
            lblEmailRecordID.Text = value
        End Set
    End Property

    Public Property RecordID() As Int32
        Get
            Return lblRecordID.Text
        End Get
        Set(ByVal value As Int32)
            lblRecordID.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'LoadType(lblEmailModuleType.Text)
            'Sets Original Body Text. For pages that don't redirect to themselves on Delete, Save, or Cancel. (See ctrlQA or ctrlMessagesandDocs)
            lblOriginalBodyText.Text = reContentText.Content
        End If
    End Sub

    Public Sub SetOriginalBodyText()
        reContentText.Content = lblOriginalBodyText.Text
    End Sub

    Public Sub LoadType(ByVal strEmailModuleType As String)

        Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommand As String = "ProjectEmailModuleTypeSelect"
        Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

        'Select Case strEmailModuleType

        '    'Contract Pages
        '    Case "ContractMod"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "ContractMod")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectContractMod '12
        '    Case "TOQandA"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "TOQandA")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectQA '7
        '    Case "TOTEPCom"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "TOTEPCom")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectTaskOrdeProjectCom '13

        '        'Project Pages
        '    Case "Project"
        '        Dim dbProject As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        '        Dim sqlcommandProject As String = "ProjectEmailCheckSelect"
        '        Dim objcommandProject As DbCommand = dbProject.GetStoredProcCommand(sqlcommandProject)
        '        dbProject.AddInParameter(objcommandProject, "@RecordTypeID", DbType.Int32, clsEmail.RecordType.Project)
        '        dbProject.AddInParameter(objcommandProject, "@RecordID", DbType.Int32, lblRecordID.Text)

        '        Using DatareaderProject As IDataReader = dbProject.ExecuteReader(objcommandProject)
        '            Dim bHasRows As Boolean = False

        '            While DatareaderProject.Read
        '                bHasRows = True
        '                reContentText.Content = DatareaderProject("EmailBody")
        '                txtTitle.Text = DatareaderProject("EmailSubject")
        '            End While
        '            If bHasRows Then
        '                lblRecordTypeID.Text = clsEmail.RecordType.Project '8
        '                DatareaderProject.Close()
        '                Exit Sub
        '            Else
        '                db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "Project")
        '                lblRecordTypeID.Text = clsEmail.RecordType.Project '8
        '            End If
        '        End Using
        '    Case "ProjectNew"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "ProjectNew")
        '        lblRecordTypeID.Text = clsEmail.RecordType.Project '8
        '        'set boolean so body and subject are not built until save
        '        bNewProject = True
        '        'set the warning to visible true
        '        lblEmailWarning.Visible = True
        '    Case "ProjectRevision"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "ProjectRevision")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectRevision '5
        '    Case "ProjectMessagesDocuments"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "ProjectMessagesDocuments")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectMessagesDocuments '6
        '    Case "ProjectQandA"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "ProjectQandA")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectQA '7
        '        bProjectQandA = True
        '    Case "ProjectTEPCom"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "ProjectTEPCom")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectTEPCom '9
        '    Case "GeneralAnnouncement"
        '        db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "GeneralAnnouncement")
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectGeneralAnnouncement

        '        'Option Pages
        '        'Case "Option"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "Option")
        '        '    lblRecordTypeID.Text = "10"
        '        'Case "OptionNew"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "OptionNew")
        '        '    lblRecordTypeID.Text = "10"
        '        'Case "OptionRevision"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "OptionRevision")
        '        '    lblRecordTypeID.Text = "5"
        '        'Case "OptionMessagesDocuments"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "OptionMessagesDocuments")
        '        '    lblRecordTypeID.Text = "6"
        '        'Case "OptionQandA"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "OptionQandA")
        '        '    lblRecordTypeID.Text = "7"
        '        'Case "OptionTEPCom"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "OptionTEPCom")
        '        '    lblRecordTypeID.Text = "11"
        '        'Case "MarketReasearchQandA"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "QandA")
        '        '    lblRecordTypeID.Text = "5"
        '        '    'Generic, incase proper values aren't getting passed
        '        'Case "MessagesDocuments"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "MessagesDocuments")
        '        '    lblRecordTypeID.Text = "6"
        '        'Case "QandA"
        '        '    db.AddInParameter(objCommand, "@ProjectEmailModuleTypeName", DbType.String, "QandA")
        '        '    lblRecordTypeID.Text = "5"

        '    Case Else
        '        reContentText.Content = "This is the text that will display if no EmailModuleType is provided."
        '        lblRecordTypeID.Text = clsEmail.RecordType.ProjectGeneric '14
        '        Exit Sub
        'End Select


        Using Datareader As IDataReader = db.ExecuteReader(objCommand)
            While Datareader.Read
                reContentText.Content = Datareader("EmailBody")
                txtTitle.Text = Datareader("EmailSubject")
            End While

        End Using

        'build the email body and subject 
        'if new Project build subject for new
        If bNewProject Then
            BuildProjectNewSubject()
        Else
            BuildProjectBody()
            BuildProjectSubject()
        End If
        'ReplaceIDIQDisplayNames()

        If strEmailModuleType = "GeneralAnnouncement" Then
            BuildGeneralAnnouncement()
        End If


    End Sub

    Private Sub chkEmail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEmail.CheckedChanged
        If chkEmail.Checked = True Then
            pnlEmail.Visible = True
        Else
            pnlEmail.Visible = False
        End If

    End Sub
    Public Sub BuildGeneralAnnouncement()
        reContentText.Content = Regex.Replace(reContentText.Content, "##IDIQName", lblIDIQName.Text)
    End Sub
    Public Sub BuildProjectBody()
        'build the email body
        Dim strEmailBody As String = reContentText.Content
        strEmailBody = Regex.Replace(strEmailBody, "##ProjectTitle", lblProjectTItle.Text)
        strEmailBody = Regex.Replace(strEmailBody, "##ProjectNumber", lblProjectNumber.Text)
        strEmailBody = Regex.Replace(strEmailBody, "##IDIQName", lblIDIQName.Text)
        strEmailBody = Regex.Replace(strEmailBody, "##QuestionDateTime ", lblQADate.Text)
        reContentText.Content = strEmailBody
    End Sub
    Public Sub BuildProjectSubject()
        Dim strEmailSubject As String = txtTitle.Text
        strEmailSubject = Regex.Replace(strEmailSubject, "##IDIQName", lblIDIQName.Text)
        strEmailSubject = Regex.Replace(strEmailSubject, "##ProjectNumber", lblProjectNumber.Text)
        txtTitle.Text = strEmailSubject
    End Sub
    Private Sub BuildProjectNewSubject()
        Dim strEmailSubject As String = txtTitle.Text
        strEmailSubject = Regex.Replace(strEmailSubject, "##IDIQName", lblIDIQName.Text)
        txtTitle.Text = strEmailSubject
    End Sub
    'Private Sub SendContractEmail()

    '    objEmail.RecordTypeId = lblRecordTypeID.Text
    '    objEmail.RecordId = lblEmailRecordID.Text
    '    'objEmail.EmailFrom = "Tridec@tridectech.com"
    '    objEmail.EmailHTML = True
    '    objEmail.EmailSubject = txtTitle.Text
    '    objEmail.EmailBody = reContentText.Content

    '    'if it is ContractMod, ContractTOQA or ContractTOTEPCommunication 
    '    'get all emails associated with the Contract with permission of Admin or View
    '    Dim dbEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandEmail As String = "ProjectContractEmailVendorSelect"
    '    Dim objCommandEmail As DbCommand = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    dbEmail.AddInParameter(objCommandEmail, "@ProjectContractID", DbType.Int32, lblRecordID.Text)

    '    Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)
    '        While datareaderEmail.Read()
    '            objEmail.EmailTo = datareaderEmail("Email")
    '            'If error builds string of error messages
    '            strReturn += objEmail.SendEmail() & "<br />"
    '        End While
    '    End Using

    '    Dim intProjectTaskOrderID As Integer = Val(Request("TaskOrderId"))
    '    If intProjectTaskOrderID > 0 Then

    '        Dim intProjectIDIQID As Integer = Val(Request("IDIQID"))
    '        Dim strIDIQName As String = getIDIQName(intProjectIDIQID)

    '        'PUll all Admin subscribed to Project emails and send
    '        dbEmail = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '        sqlcommandEmail = "ProjectAdminEmailSubscriptionSelect"
    '        objCommandEmail = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '        dbEmail.AddInParameter(objCommandEmail, "@AdminName", DbType.String, strIDIQName & "Admin")
    '        dbEmail.AddInParameter(objCommandEmail, "@TeamName", DbType.String, strIDIQName & "Team")
    '        dbEmail.AddInParameter(objCommandEmail, "@PreName", DbType.String, strIDIQName & "Pre")
    '        dbEmail.AddInParameter(objCommandEmail, "@PostName", DbType.String, strIDIQName & "Post")
    '        dbEmail.AddInParameter(objCommandEmail, "@ProjectTaskOrderID", DbType.Int32, intProjectTaskOrderID)
    '        dbEmail.AddInParameter(objCommandEmail, "@ProjectAdminEmailSubscriptionTypeID", DbType.Int32, clsProjectEmailSubscription.AdminEmailSubscriptionType.Vendor)
    '        Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)

    '            While datareaderEmail.Read()
    '                objEmail.EmailTo = datareaderEmail("Email")
    '                'If error builds string of error messages
    '                strReturn += objEmail.SendEmail() & "<br />"
    '            End While
    '        End Using
    '        'Returns the list of error messages if any

    '    End If

    'End Sub

    Private Sub SendProjectEmail()

        objEmail.RecordTypeId = lblRecordTypeID.Text
        objEmail.RecordId = lblEmailRecordID.Text
        objEmail.EmailHTML = True
        objEmail.EmailSubject = txtTitle.Text
        objEmail.EmailBody = reContentText.Content

        'If it is Project Details, New, Revision, or Message and docs
        'Pull all vendors assigned to Project with permission of Admin or View

        Dim dbEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
        Dim sqlcommandEmail As String = "ProjectEmailVendorSelect"
        Dim objCommandEmail As DbCommand = dbEmail.GetStoredProcCommand(sqlcommandEmail)
        'dbEmail = DatabaseFactory.CreateDatabase("VOAConnectionString")
        'dbEmail.AddInParameter(objCommandEmail, "@ProjectID", DbType.Int32, lblRecordID.Text)
        'Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)

        '    While datareaderEmail.Read()
        '        objEmail.EmailTo = datareaderEmail("Email")
        '        'If error builds string of error messages
        '        strReturn += objEmail.SendEmail() & "<br />"
        '    End While
        'End Using

        'Dim intProjectIDIQID As Integer = Val(Request("IDIQID"))
        'Dim strIDIQName As String = getIDIQName(intProjectIDIQID)

        'PUll all Admin subscribed to Project emails and send
        dbEmail = DatabaseFactory.CreateDatabase("VOAConnectionString")
        sqlcommandEmail = "ProjectAdminEmailSubscriptionSelect"
        objCommandEmail = dbEmail.GetStoredProcCommand(sqlcommandEmail)
        'dbEmail.AddInParameter(objCommandEmail, "@AdminName", DbType.String, strIDIQName & "Admin")
        'dbEmail.AddInParameter(objCommandEmail, "@TeamName", DbType.String, strIDIQName & "Team")
        'dbEmail.AddInParameter(objCommandEmail, "@PreName", DbType.String, strIDIQName & "Pre")
        'dbEmail.AddInParameter(objCommandEmail, "@PostName", DbType.String, strIDIQName & "Post")
        dbEmail.AddInParameter(objCommandEmail, "@ProjectID", DbType.Int32, lblRecordID.Text)
        'dbEmail.AddInParameter(objCommandEmail, "@ProjectAdminEmailSubscriptionTypeID", DbType.Int32, clsProjectEmailSubscription.AdminEmailSubscriptionType.Vendor)
        Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)

            While datareaderEmail.Read()
                objEmail.EmailTo = datareaderEmail("Email")
                'If error builds string of error messages
                strReturn += objEmail.SendEmail() & "<br />"
            End While
        End Using

    End Sub

    'Private Sub SendGeneralAnnouncementEmail()
    '    'If it is Project Details, New, Revision, or Message and docs
    '    'Pull all vendors assigned to Project with permission of Admin or View
    '    Dim dbEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandEmail As String = "ProjectGeneralAnnouncementEmailVendorSelect"
    '    Dim objCommandEmail As DbCommand = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    dbEmail.AddInParameter(objCommandEmail, "@ProjectGeneralAnnouncementID", DbType.Int32, lblRecordID.Text)

    '    Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)
    '        While datareaderEmail.Read()
    '            objEmail.EmailTo = datareaderEmail("Email")
    '            objEmail.RecordTypeId = lblRecordTypeID.Text
    '            objEmail.RecordId = lblEmailRecordID.Text
    '            objEmail.EmailHTML = True
    '            objEmail.EmailSubject = txtTitle.Text
    '            objEmail.EmailBody = reContentText.Content
    '            'If error builds string of error messages
    '            strReturn += objEmail.SendEmail() & "<br />"
    '        End While
    '    End Using
    'End Sub
    'Private Sub SendProjectEmailQA()
    '    'If Q and A or Tep Com send only to the selected Vendor
    '    objEmail.RecordTypeId = lblRecordTypeID.Text
    '    objEmail.RecordId = lblEmailRecordID.Text
    '    'objEmail.EmailFrom = "Tridec@tridectech.com"
    '    objEmail.EmailHTML = True
    '    objEmail.EmailSubject = txtTitle.Text
    '    objEmail.EmailBody = reContentText.Content

    '    Dim dbEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandEmail As String = "ProjectEmailVendorSelect"
    '    Dim objCommandEmail As DbCommand = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    'dbEmail.AddInParameter(objCommandEmail, "@ProjectID", DbType.Int32, lblRecordID.Text)
    '    dbEmail.AddInParameter(objCommandEmail, "@ProjectVendorID", DbType.Int32, lblProjectVendorID.Text)

    '    Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)
    '        While datareaderEmail.Read()
    '            objEmail.EmailTo = datareaderEmail("Email")
    '            'If error builds string of error messages
    '            strReturn += objEmail.SendEmail() & "<br />"
    '        End While
    '    End Using

    '    Dim intProjectIDIQID As Integer = Val(Request("IDIQID"))
    '    Dim strIDIQName As String = getIDIQName(intProjectIDIQID)

    '    'PUll all Admin subscribed to Project emails and send
    '    dbEmail = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    sqlcommandEmail = "ProjectAdminEmailSubscriptionSelect"
    '    objCommandEmail = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    dbEmail.AddInParameter(objCommandEmail, "@AdminName", DbType.String, strIDIQName & "Admin")
    '    dbEmail.AddInParameter(objCommandEmail, "@TeamName", DbType.String, strIDIQName & "Team")
    '    dbEmail.AddInParameter(objCommandEmail, "@PreName", DbType.String, strIDIQName & "Pre")
    '    dbEmail.AddInParameter(objCommandEmail, "@ProjectID", DbType.Int32, lblRecordID.Text)
    '    dbEmail.AddInParameter(objCommandEmail, "@ProjectAdminEmailSubscriptionTypeID", DbType.Int32, clsProjectEmailSubscription.AdminEmailSubscriptionType.Vendor)
    '    Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)

    '        While datareaderEmail.Read()
    '            objEmail.EmailTo = datareaderEmail("Email")
    '            'If error builds string of error messages
    '            strReturn += objEmail.SendEmail() & "<br />"
    '        End While
    '    End Using

    'End Sub

    'Private Function getIDIQName(ByVal ProjectIDIQID As Integer) As String

    '    'Gets the Name of the IDIQ and sets the names for admin and team
    '    Dim strResult As String = ""

    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "ProjectIDIQDetailsSelect"
    '    Dim objCommand As DbCommand = db.GetStoredProcCommand(sqlcommand)
    '    db.AddInParameter(objCommand, "ProjectIDIQID", DbType.Int32, ProjectIDIQID)

    '    Using datareader As IDataReader = db.ExecuteReader(objCommand)
    '        If datareader.Read() Then
    '            strResult = datareader("Name")
    '        End If
    '    End Using

    '    Return strResult

    'End Function

    'Private Function isHierarchical(ByVal ProjectIDIQID As Integer) As Boolean

    '    Dim blnResult As Boolean = False

    '    Dim db As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommand As String = "ProjectNewPOCSelect"
    '    Dim objcommand As DbCommand = db.GetStoredProcCommand(sqlcommand)

    '    db.AddInParameter(objcommand, "@ProjectIDIQID", DbType.Int32, ProjectIDIQID)

    '    Using datareader As IDataReader = db.ExecuteReader(objcommand)
    '        If datareader.Read() Then

    '            If Val(datareader("ContractingHierarchy")) = 1 Then
    '                blnResult = True
    '            End If

    '        End If
    '    End Using

    '    Return blnResult

    'End Function

    'Private Sub subscribeVendorsForNewProject()

    '    Dim dbAllEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandAllEmail As String = "ProjectEmailVendorSelect"
    '    Dim objCommandAllEmail As DbCommand = dbAllEmail.GetStoredProcCommand(sqlcommandAllEmail)
    '    dbAllEmail.AddInParameter(objCommandAllEmail, "@ProjectID", DbType.Int32, lblRecordID.Text)
    '    dbAllEmail.AddInParameter(objCommandAllEmail, "@getALL", DbType.Int32, 1)
    '    Using datareaderEmail As IDataReader = dbAllEmail.ExecuteReader(objCommandAllEmail)

    '        While datareaderEmail.Read()
    '            clsProjectEmailSubscription.addVendorSubscription(datareaderEmail("UserId").ToString, lblRecordID.Text)
    '        End While
    '    End Using

    'End Sub

    'Private Sub subscribeAdminForNewProject()

    '    Dim intProjectIDIQID As Integer = Val(Request("IDIQID"))
    '    Dim intProjectID As Integer = Val(lblRecordID.Text)

    '    If intProjectIDIQID = 0 Then
    '        Return
    '    End If

    '    Dim strIDIQAdminName As String = getIDIQName(intProjectIDIQID)

    '    'Gets the email of each user in the admin and assigns them Admin emails for this ProjectID 

    '    Dim dbEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandEmail As String = "ProjectAdminTeamSelect"
    '    Dim objCommandEmail As DbCommand = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    dbEmail.AddInParameter(objCommandEmail, "AdminName", DbType.String, strIDIQAdminName & "Admin")
    '    Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)
    '        While datareaderEmail.Read
    '            clsProjectEmailSubscription.addAdminSubscription(datareaderEmail("UserID").ToString, intProjectID, 0, clsProjectEmailSubscription.AdminEmailSubscriptionType.Project)
    '        End While
    '    End Using

    '    'Get the email COs and CSs

    '    dbEmail = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    sqlcommandEmail = "ProjectAdminCOCSSubscriptionSelect"
    '    objCommandEmail = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    dbEmail.AddInParameter(objCommandEmail, "@ProjectIDIQID", DbType.Int32, intProjectIDIQID)
    '    If isHierarchical(intProjectIDIQID) Then
    '        dbEmail.AddInParameter(objCommandEmail, "@ProjectID", DbType.Int32, intProjectID)
    '    End If
    '    Using datareaderEmail2 As IDataReader = dbEmail.ExecuteReader(objCommandEmail)
    '        While datareaderEmail2.Read
    '            clsProjectEmailSubscription.addAdminSubscription(datareaderEmail2("UserID").ToString, intProjectID, 0, clsProjectEmailSubscription.AdminEmailSubscriptionType.Project)
    '        End While
    '    End Using

    'End Sub

    'Private Sub subscribeAdminForNewTaskOrder(ByVal NewTaskOrderID As Integer)

    '    Dim intProjectIDIQID As Integer = Val(Request("IDIQID"))

    '    If intProjectIDIQID = 0 Then
    '        Return
    '    End If

    '    Dim strIDIQAdminName As String = getIDIQName(intProjectIDIQID)

    '    'Gets the email of each user in the admin and assigns them Admin emails for this ProjectID 

    '    Dim dbEmail As Database = DatabaseFactory.CreateDatabase("VOAConnectionString")
    '    Dim sqlcommandEmail As String = "ProjectAdminTeamSelect"
    '    Dim objCommandEmail As DbCommand = dbEmail.GetStoredProcCommand(sqlcommandEmail)
    '    dbEmail.AddInParameter(objCommandEmail, "AdminName", DbType.String, strIDIQAdminName & "Admin")
    '    dbEmail.AddInParameter(objCommandEmail, "TeamName", DbType.String, strIDIQAdminName & "Team")
    '    dbEmail.AddInParameter(objCommandEmail, "PostName", DbType.String, strIDIQAdminName & "Post")
    '    Using datareaderEmail As IDataReader = dbEmail.ExecuteReader(objCommandEmail)
    '        While datareaderEmail.Read
    '            clsProjectEmailSubscription.addAdminSubscription(datareaderEmail("UserID").ToString, 0, NewTaskOrderID, clsProjectEmailSubscription.AdminEmailSubscriptionType.TaskOrder)
    '        End While
    '    End Using

    'End Sub

    Public Function SendMail(Optional ByVal NewTaskOrderID As Integer = 0) As String

        'If New Project, Subscribe ALL assigned vendors to Project 
        'If lblEmailModuleType.Text = "ProjectNew" Then
        '    subscribeVendorsForNewProject()
        '    subscribeAdminForNewProject()
        'End If

        'if new Task Order, Subscribe Admin/TeamPost
        'If NewTaskOrderID > 0 Then
        '    subscribeAdminForNewTaskOrder(NewTaskOrderID)
        'End If

        If pnlEmail.Visible Then
            'ReplaceIDIQDisplayNames()
            'If String.Equals(lblEmailModuleType.Text, "ContractMod", StringComparison.OrdinalIgnoreCase) _
            'Or String.Equals(lblEmailModuleType.Text, "TOQandA", StringComparison.OrdinalIgnoreCase) _
            'Or String.Equals(lblEmailModuleType.Text, "TOTEPCom", StringComparison.OrdinalIgnoreCase) Then
            'SendContractEmail()
            'Return strReturn
            If String.Equals(lblEmailModuleType.Text, "ProjectNew", StringComparison.OrdinalIgnoreCase) Then
                'build the email body
                BuildProjectBody()
                BuildProjectSubject()
                SendProjectEmail()
                Return strReturn
            ElseIf String.Equals(lblEmailModuleType.Text, "Project", StringComparison.OrdinalIgnoreCase) _
            Or String.Equals(lblEmailModuleType.Text, "ProjectRevision", StringComparison.OrdinalIgnoreCase) _
            Or String.Equals(lblEmailModuleType.Text, "ProjectMessagesDocuments", StringComparison.OrdinalIgnoreCase) Then
                SendProjectEmail()
                Return strReturn
                'ElseIf String.Equals(lblEmailModuleType.Text, "ProjectQandA", StringComparison.OrdinalIgnoreCase) _
                'Or String.Equals(lblEmailModuleType.Text, "ProjectTEPCom", StringComparison.OrdinalIgnoreCase) Then
                '    SendProjectEmailQA()
                '    Return strReturn
                'ElseIf String.Equals(lblEmailModuleType.Text, "Option", StringComparison.OrdinalIgnoreCase) _
                'Or String.Equals(lblEmailModuleType.Text, "OptionNew", StringComparison.OrdinalIgnoreCase) _
                'Or String.Equals(lblEmailModuleType.Text, "OptionRevision", StringComparison.OrdinalIgnoreCase) _
                'Or String.Equals(lblEmailModuleType.Text, "OptionMessagesDocuments", StringComparison.OrdinalIgnoreCase) Then
                '    SendOptionEmail()
                '    Return strReturn
                'ElseIf String.Equals(lblEmailModuleType.Text, "OptionQandA", StringComparison.OrdinalIgnoreCase) _
                'Or String.Equals(lblEmailModuleType.Text, "OptionTEPCom", StringComparison.OrdinalIgnoreCase) Then
                '    SendOptionEmailQA()
                '    Return strReturn
                'ElseIf String.Equals(lblEmailModuleType.Text, "MarketReasearchQandA", StringComparison.OrdinalIgnoreCase) Then
                '    SendMarketResearchEmailQandA()
                '    Return strReturn
                'ElseIf String.Equals(lblEmailModuleType.Text, "GeneralAnnouncement", StringComparison.OrdinalIgnoreCase) Then
                '    SendGeneralAnnouncementEmail()
                '    Return strReturn
            Else
                objEmail.RecordTypeId = lblRecordTypeID.Text
                objEmail.RecordId = lblEmailRecordID.Text
                objEmail.EmailTo = lblEmailTo.Text
                'objEmail.EmailFrom = "Tridec@tridectech.com"
                objEmail.EmailHTML = True
                objEmail.EmailSubject = txtTitle.Text
                objEmail.EmailBody = reContentText.Content
                Return objEmail.SendEmail()
            End If
        Else
            Return ""
        End If
    End Function
    'Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '    pnlEmail.Visible = False
    '    chkEmail.Checked = False
    'End Sub

    'Private Sub ReplaceIDIQDisplayNames()
    '    Dim IDIQDisplayNames As New clsIDIQDisplayNames(Val(Request("IDIQID")))

    '    Dim strEmailSubject As String = txtTitle.Text
    '    strEmailSubject = Regex.Replace(strEmailSubject, "##ProjectDisplayName", IDIQDisplayNames.ProjectDisplayName)
    '    strEmailSubject = Regex.Replace(strEmailSubject, "##TEPDisplayName", IDIQDisplayNames.TEPDisplayName)
    '    txtTitle.Text = strEmailSubject

    '    Dim strEmailBody As String = reContentText.Content
    '    strEmailBody = Regex.Replace(strEmailBody, "##ProjectDisplayName", IDIQDisplayNames.ProjectDisplayName)
    '    strEmailBody = Regex.Replace(strEmailBody, "##TEPDisplayName", IDIQDisplayNames.TEPDisplayName)
    '    reContentText.Content = strEmailBody

    '    lblEmailWarning.Text = "Fields ##ProjectNumber, ##ProjectTitle and  will be filled in upon save with the new " & IDIQDisplayNames.ProjectDisplayName & " details."
    'End Sub

End Class