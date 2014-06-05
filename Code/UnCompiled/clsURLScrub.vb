Public Class clsURLScrub
    Inherits System.Web.UI.Page

    Public Function URLScrub(ByVal strURLTest As String)
        Dim valid As New Boolean
        Dim blacklist As New Collection
        Dim spacelist As New Collection
        'Dim investlist As New Collection

        strURLTest = LCase(strURLTest)

        valid = True
        If Len(strURLTest) > 0 Then

            ' Commented out because spaces were used in OLD RAD TreeView 
            ' check url for space of any kind, a space is not allowed
            spacelist.Add("%20") 'hex space
            'spacelist.Add("")   'oct space
            spacelist.Add("chr(32)") 'html space
            For Each itm As String In spacelist
                If strURLTest.IndexOf(itm) <> -1 Then
                    'bad string found
                    valid = False
                    Exit For
                End If
            Next
        End If
        If valid = True Then
            'Fill out blacklist with words in lower case only
            blacklist.Add("--")
            blacklist.Add("+")  'new 03-06-09 JMT
            blacklist.Add(":")
            blacklist.Add("%3a") ' url encode for :
            blacklist.Add(";")
            blacklist.Add("%3b") ' url encode for ;
            blacklist.Add("@@")
            blacklist.Add("'")
            blacklist.Add("%27")  'url encode for '
            blacklist.Add("|")
            blacklist.Add("^")
            blacklist.Add("~")
            blacklist.Add("%7e")  'url encode for ~
            blacklist.Add("<>")
            blacklist.Add("!")
            blacklist.Add("/")
            blacklist.Add("%2F")  'url encode for /
            blacklist.Add(")")
            blacklist.Add("%29")  'url encode for )
            blacklist.Add("/*")
            blacklist.Add("*/")
            blacklist.Add("%22")  ' url encode for "
            blacklist.Add("""")
            blacklist.Add("all_")
            blacklist.Add("alter%20database")
            blacklist.Add("alter%20function")
            blacklist.Add("alter%20procedure")
            blacklist.Add("alter%20table")
            blacklist.Add("alter%20trigger")
            blacklist.Add("alter%20view")
            blacklist.Add("appl_")
            blacklist.Add("ascii(")
            blacklist.Add("auth_")
            blacklist.Add("bcp")
            blacklist.Add("begin%20distributed%20transaction")
            blacklist.Add("begin%20transaction")
            blacklist.Add("binary_checksum")
            blacklist.Add("bulk")
            blacklist.Add("bulk%20insert")
            blacklist.Add("charindex")
            blacklist.Add("cert_")
            blacklist.Add("checkpoint")
            blacklist.Add("checksum")
            blacklist.Add("checksum_agg")
            blacklist.Add("cmdshell")
            blacklist.Add("coalesce")
            blacklist.Add("collate")
            blacklist.Add("collationproperty")
            blacklist.Add("col_length")
            blacklist.Add("col_name")
            blacklist.Add("columnproperty")
            blacklist.Add("commit%20transaction")
            blacklist.Add("commit%20work")
            blacklist.Add("containstable")
            blacklist.Add("content_")
            blacklist.Add("count_big")
            blacklist.Add("create%20database")
            blacklist.Add("create%20function")
            blacklist.Add("create%20index")
            blacklist.Add("create%20procedure")
            blacklist.Add("create%20rule")
            blacklist.Add("create%20schema")
            blacklist.Add("create%20statistics")
            blacklist.Add("create%20table")
            blacklist.Add("create%20trigger")
            blacklist.Add("create%20view")
            blacklist.Add("current_timestamp")
            blacklist.Add("current_user")
            blacklist.Add("cursor_status")
            blacklist.Add("databaseproperty")
            blacklist.Add("databasepropertyex")
            blacklist.Add("datalength")
            blacklist.Add("dateadd")
            blacklist.Add("datediff")
            blacklist.Add("datename")
            blacklist.Add("datepart")
            blacklist.Add("db_id")
            blacklist.Add("dbcc")
            blacklist.Add("declare @")
            blacklist.Add("drop%20database")
            blacklist.Add("drop%20default")
            blacklist.Add("drop%20function")
            blacklist.Add("drop%20index")
            blacklist.Add("drop%20procedure")
            blacklist.Add("drop%20rule")
            blacklist.Add("drop%20statistics")
            blacklist.Add("drop%20table")
            blacklist.Add("drop%20trigger")
            blacklist.Add("drop%20view")
            blacklist.Add("dump")
            blacklist.Add("exec")
            blacklist.Add("file_id")
            blacklist.Add("file_name")
            blacklist.Add("filegroup_id")
            blacklist.Add("filegroup_name")
            blacklist.Add("filegroupproperty")
            blacklist.Add("fileproperty")
            blacklist.Add("fn_helpcollations")
            blacklist.Add("fn_listextendedproperty")
            blacklist.Add("fn_servershareddrives")
            blacklist.Add("fn_trace_geteveninfo")
            blacklist.Add("fn_trace_getfilterinfo")
            blacklist.Add("fn_trace_getinfo")
            blacklist.Add("fn_trace_gettable")
            blacklist.Add("fn_virtualfilestats")
            blacklist.Add("fn_virtualservernodes")
            blacklist.Add("formatmessage")
            blacklist.Add("freetext")
            blacklist.Add("freetexttable")
            blacklist.Add("fulltextcatalogproperty")
            blacklist.Add("fulltextserviceproperty")
            blacklist.Add("gateway_")
            blacklist.Add("getansinull")
            blacklist.Add("group bye")
            blacklist.Add("has_dbaccess")
            blacklist.Add("host_id")
            blacklist.Add("host_name")
            blacklist.Add("http_")
            blacklist.Add("https_")
            blacklist.Add("ident_current")
            blacklist.Add("ident_incr")
            blacklist.Add("ident_seed")
            blacklist.Add("in(")
            blacklist.Add("indexkey_property")
            blacklist.Add("indexproperty")
            blacklist.Add("index_col")
            blacklist.Add("information_schema")
            blacklist.Add("instance_")
            blacklist.Add("is_member")
            blacklist.Add("is_srvrolemember")
            blacklist.Add("is[not]null")
            blacklist.Add("isnull")
            blacklist.Add("is%20null")
            blacklist.Add("left(")
            blacklist.Add("len(")
            blacklist.Add("local_")
            blacklist.Add("logon_")
            blacklist.Add("lower(")
            blacklist.Add("ltrim(")
            blacklist.Add("max(")
            blacklist.Add("min(")
            blacklist.Add("month(")
            blacklist.Add("nchar(")
            blacklist.Add("newid")
            blacklist.Add("nullif")
            blacklist.Add("object_id")
            blacklist.Add("object_name")
            blacklist.Add("objectproperty")
            blacklist.Add("opendatasource")
            blacklist.Add("openquery")
            blacklist.Add("openrowset")
            blacklist.Add("openxml")
            blacklist.Add("order%20by")
            blacklist.Add("parsename")
            blacklist.Add("path_")
            blacklist.Add("patindex")
            blacklist.Add("query_")
            blacklist.Add("quotename")
            blacklist.Add("raiserror")
            blacklist.Add("reconfigure")
            blacklist.Add("remote_")
            blacklist.Add("replicate(")
            blacklist.Add("request_")
            blacklist.Add("restore%20filelistonly")
            blacklist.Add("restore%20headeronly")
            blacklist.Add("restore%20labelonly")
            blacklist.Add("restore%20verifyonly")
            blacklist.Add("reverse(")
            blacklist.Add("revoke all")
            blacklist.Add("right(")
            blacklist.Add("rollback%20transaction")
            blacklist.Add("rollback%20work")
            blacklist.Add("rowcount_big")
            blacklist.Add("rtrim(")
            blacklist.Add("save%20transaction")
            blacklist.Add("scope_identity")
            blacklist.Add("script_")
            blacklist.Add("server_")
            blacklist.Add("serverproperty")
            blacklist.Add("session_user")
            blacklist.Add("sessionproperty")
            blacklist.Add("setuser")
            blacklist.Add("shutdown")
            blacklist.Add("sp_")
            blacklist.Add("space(")
            blacklist.Add("sql_variant_property")
            blacklist.Add("stats_date")
            blacklist.Add("substring")
            blacklist.Add("suser_id")
            blacklist.Add("suser_name")
            blacklist.Add("suser_sid")
            blacklist.Add("suser_sname")
            blacklist.Add("syscolumns")
            blacklist.Add("sysobjects")
            blacklist.Add("system_user")
            blacklist.Add("textptr")
            blacklist.Add("textvalid")
            blacklist.Add("trigger_nestlevel")
            blacklist.Add("truncate%20table")
            blacklist.Add("typeproperty")
            blacklist.Add("update%20statistics")
            blacklist.Add("updatetext")
            blacklist.Add("upper(")
            blacklist.Add("waitfor")
            blacklist.Add("writetext")
            blacklist.Add("xp_")
            'check url for + and if there is a plus next to our investigate list
            'blacklist.Add("having")
            'blacklist.Add("or")
            'blacklist.Add("url")
            'blacklist.Add("select")
            'blacklist.Add("grant")
            blacklist.Add("@")

            For Each itm As String In blacklist
                If strURLTest.IndexOf(itm) <> -1 Then
                    'bad string found
                    valid = False
                    Exit For
                End If
            Next
        End If
        'Dim intLength As Integer
        'intLength = Len(strURLTest)
        'Dim intX As Integer
        'intX = 0

        'While intX < intLength
        '    If intX = 0 Then
        '        intX = strURLTest.IndexOf("+")
        '        If intX = -1 Then
        '            Exit While
        '        End If
        '    Else
        '        intX = strURLTest.IndexOf("+", intX)
        '        If intX = -1 Then
        '            Exit While
        '        End If
        '    End If

        '    Dim intLengthItem As Integer

        '    'loops through all the investigate word list
        '    For Each itm As String In investlist
        '        intLengthItem = Len(itm)
        '        If strURLTest.IndexOf(itm, intX - intLengthItem, intLengthItem) <> -1 Then
        '            'if the word is found it will return the position of the first occurence starting at intX.
        '            'see if the plus then investigate word ie. having+
        '            Dim strPartWord As String
        '            strPartWord = Mid(strURLTest, intX - (intLengthItem - 1), intLengthItem)
        '            If strPartWord = itm Then
        '                'invalid word found before the + exit
        '                valid = False
        '                Exit For
        '            End If
        '        End If
        '    Next
        '    intX = intX + 1
        'End While

        If valid = False Then
            System.Web.HttpContext.Current.Response.Redirect("/ErrorPage.aspx")
            Return False
        Else
            Return True
        End If

    End Function

End Class
