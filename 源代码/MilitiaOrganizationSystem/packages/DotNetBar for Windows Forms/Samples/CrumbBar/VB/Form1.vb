Public Class Form1

    Private Sub CrumbBarSelectedItemChanging(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.CrumbBarSelectionEventArgs) Handles crumbBar1.SelectedItemChanging
        ' Note that at this point you can cancel the selected item change by setting
        ' e.Cancel=true
        AddToLog("SelectedItemChanging", CType(sender, DevComponents.DotNetBar.CrumbBar), e.NewSelectedItem)
    End Sub

    Private Sub CrumbBarSelectedItemChanged(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.CrumbBarSelectionEventArgs) Handles crumbBar1.SelectedItemChanged, crumbBar2.SelectedItemChanged
        AddToLog("SelectedItemChanged", CType(sender, DevComponents.DotNetBar.CrumbBar), e.NewSelectedItem)
    End Sub

    Private Sub crumbBar2_SelectedItemChanging(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.CrumbBarSelectionEventArgs) Handles crumbBar2.SelectedItemChanging
        AddToLog("SelectedItemChanging", CType(sender, DevComponents.DotNetBar.CrumbBar), e.NewSelectedItem)

        ' Load folders are selection changes...
        Dim parent As DevComponents.DotNetBar.CrumbBarItem = e.NewSelectedItem
        If parent Is Nothing OrElse parent.SubItems.Count > 0 Then
            Return
        End If

        Dim dirInfo As IO.DirectoryInfo = Nothing
        If TypeOf parent.Tag Is IO.DriveInfo Then
            Dim driveInfo As IO.DriveInfo = CType(parent.Tag, IO.DriveInfo)
            dirInfo = driveInfo.RootDirectory
        ElseIf TypeOf parent.Tag Is IO.DirectoryInfo Then
            dirInfo = CType(parent.Tag, IO.DirectoryInfo)
        End If

        Dim subDirectories() As IO.DirectoryInfo = dirInfo.GetDirectories()
        For Each directoryInfo As IO.DirectoryInfo In subDirectories
            Dim node As New DevComponents.DotNetBar.CrumbBarItem()
            node.Tag = directoryInfo
            node.Text = directoryInfo.Name
            node.Image = Global.CrumbBar.My.Resources.Resources.folder
            parent.SubItems.Add(node)
        Next directoryInfo
    End Sub

    Private Sub AddToLog(ByVal eventName As String, ByVal crumbBar As DevComponents.DotNetBar.CrumbBar, ByVal crumbBarItem As DevComponents.DotNetBar.CrumbBarItem)
        textBoxX1.Text += DateTime.Now.ToString() & " - " & eventName & " (" & crumbBar.Name & ")"
        textBoxX1.Select(textBoxX1.Text.Length - 1, 0)
        textBoxX1.ScrollToCaret()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Load first batch of items
        Dim myComputer As New DevComponents.DotNetBar.CrumbBarItem()
        myComputer.Text = "My Computer"
        myComputer.Image = Global.CrumbBar.My.Resources.Resources.computer
        crumbBar2.Items.Add(myComputer)
        ' Load disks, we will lazy load folders are disk are selected
        Dim drives() As IO.DriveInfo = IO.DriveInfo.GetDrives()
        For Each driveInfo As IO.DriveInfo In drives
            If driveInfo.DriveType <> IO.DriveType.Fixed Then
                Continue For
            End If
            Dim node As New DevComponents.DotNetBar.CrumbBarItem()
            node.Tag = driveInfo
            node.Text = "Local Disk " & driveInfo.Name.Replace("\", "")
            node.Image = Global.CrumbBar.My.Resources.Resources.hdd
            myComputer.SubItems.Add(node)
        Next driveInfo
        ' Finally select My Computer
        crumbBar2.SelectedItem = myComputer
    End Sub
End Class
