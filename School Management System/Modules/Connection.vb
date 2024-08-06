Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Reflection


Module Connection
    Public cn As New MySqlConnection
    Public cm As New MySqlCommand

    Public cn2 As New MySqlConnection
    Public cm2 As New MySqlCommand

    Public adptr As New MySqlDataAdapter


    Public str_userid, str_user, str_name, str_role, str_school, str_address As String

    Public activeAcademicYear As Integer
    Public DateToday As String
    Public YearToday As String

    Public systemdbhost As String = "127.0.0.1"
    Public systemdbuser As String = "server"
    Public systemdbpass As String = "cronasia"
    Public systemdbport As String = "3306"


    Public strHostName As String = System.Net.Dns.GetHostName()
    Public strIPAddress As String = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal).First().ToString()

    Sub Connection1()
        Dim dbhost As String = systemdbhost
        Dim dbname As String = frmLogin.SystemDataBase.Text
        Dim dbuser As String = systemdbuser
        Dim dbpass As String = systemdbpass
        Dim dbport As String = systemdbport
        cn.ConnectionString = "Data Source='" & dbhost & "'; Database='" & dbname & "'; User='" & dbuser & "'; Password='" & dbpass & "'; Port='" & dbport & "'; Convert Zero Datetime=True"
        cn.Open()
    End Sub

    Sub Connection2()
        Dim dbhost As String = systemdbhost
        Dim dbname As String = frmLogin.SystemDataBase.Text
        Dim dbuser As String = systemdbuser
        Dim dbpass As String = systemdbpass
        Dim dbport As String = systemdbport
        cn2.ConnectionString = "Data Source='" & dbhost & "'; Database='" & dbname & "'; User='" & dbuser & "'; Password='" & dbpass & "'; Port='" & dbport & "'; Convert Zero Datetime=True"
        cn2.Open()
    End Sub
    Sub CloseConnection()
        cm.Dispose()
        cn.Close()
    End Sub
    Sub CloseConnection2()
        cm2.Dispose()
        cn2.Close()
    End Sub

    Sub appVersion(ByVal versionLabel As Label)
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        Dim assemblyName As AssemblyName = assembly.GetName()
        Dim version As Version = assemblyName.Version
        Dim fileVersion As String = version.ToString()
        versionLabel.Text = fileVersion
    End Sub

End Module
