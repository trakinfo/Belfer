﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Ten kod został wygenerowany przez narzędzie.
'     Wersja wykonawcza:4.0.30319.42000
'
'     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
'     kod zostanie ponownie wygenerowany.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "Funkcjonalność automatycznego zapisywania  My.Settings"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UserName() As String
            Get
                Return CType(Me("UserName"),String)
            End Get
            Set
                Me("UserName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("localhost")>  _
        Public Property ServerIP() As String
            Get
                Return CType(Me("ServerIP"),String)
            End Get
            Set
                Me("ServerIP") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("belfer")>  _
        Public Property DBName() As String
            Get
                Return CType(Me("DBName"),String)
            End Get
            Set
                Me("DBName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("user")>  _
        Public Property SysUser() As String
            Get
                Return CType(Me("SysUser"),String)
            End Get
            Set
                Me("SysUser") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("qfolam1kFmy9ZiX8CUH59Q==")>  _
        Public Property SysPassword() As String
            Get
                Return CType(Me("SysPassword"),String)
            End Get
            Set
                Me("SysPassword") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property IdSchoolType() As String
            Get
                Return CType(Me("IdSchoolType"),String)
            End Get
            Set
                Me("IdSchoolType") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property IdSchool() As String
            Get
                Return CType(Me("IdSchool"),String)
            End Get
            Set
                Me("IdSchool") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ClassName() As String
            Get
                Return CType(Me("ClassName"),String)
            End Get
            Set
                Me("ClassName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ObjectName() As String
            Get
                Return CType(Me("ObjectName"),String)
            End Get
            Set
                Me("ObjectName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2015/2016")>  _
        Public Property SchoolYear() As String
            Get
                Return CType(Me("SchoolYear"),String)
            End Get
            Set
                Me("SchoolYear") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property IdObsada() As String
            Get
                Return CType(Me("IdObsada"),String)
            End Get
            Set
                Me("IdObsada") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property IdBelfer() As String
            Get
                Return CType(Me("IdBelfer"),String)
            End Get
            Set
                Me("IdBelfer") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property LastMiejsceUr() As Integer
            Get
                Return CType(Me("LastMiejsceUr"),Integer)
            End Get
            Set
                Me("LastMiejsceUr") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property LastMiejsceZam() As Integer
            Get
                Return CType(Me("LastMiejsceZam"),Integer)
            End Get
            Set
                Me("LastMiejsceZam") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-3.937")>  _
        Public Property XCaliber() As Single
            Get
                Return CType(Me("XCaliber"),Single)
            End Get
            Set
                Me("XCaliber") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-7.9")>  _
        Public Property YCaliber() As Single
            Get
                Return CType(Me("YCaliber"),Single)
            End Get
            Set
                Me("YCaliber") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("chkFullName")>  _
        Public Property PrintContent() As String
            Get
                Return CType(Me("PrintContent"),String)
            End Get
            Set
                Me("PrintContent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SSLMode() As Byte
            Get
                Return CType(Me("SSLMode"),Byte)
            End Get
            Set
                Me("SSLMode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("userssl")>  _
        Public Property SysSSLUser() As String
            Get
                Return CType(Me("SysSSLUser"),String)
            End Get
            Set
                Me("SysSSLUser") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ServerSSLIP() As String
            Get
                Return CType(Me("ServerSSLIP"),String)
            End Get
            Set
                Me("ServerSSLIP") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SysSSLPassword() As String
            Get
                Return CType(Me("SysSSLPassword"),String)
            End Get
            Set
                Me("SysSSLPassword") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("39")>  _
        Public Property LeftMargin() As Integer
            Get
                Return CType(Me("LeftMargin"),Integer)
            End Get
            Set
                Me("LeftMargin") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("39")>  _
        Public Property TopMargin() As Integer
            Get
                Return CType(Me("TopMargin"),Integer)
            End Get
            Set
                Me("TopMargin") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Times New Roman, 9.75pt")>  _
        Public Property TextFont() As Global.System.Drawing.Font
            Get
                Return CType(Me("TextFont"),Global.System.Drawing.Font)
            End Get
            Set
                Me("TextFont") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Arial, 12pt, style=Bold")>  _
        Public Property HeaderFont() As Global.System.Drawing.Font
            Get
                Return CType(Me("HeaderFont"),Global.System.Drawing.Font)
            End Get
            Set
                Me("HeaderFont") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Landscape() As Boolean
            Get
                Return CType(Me("Landscape"),Boolean)
            End Get
            Set
                Me("Landscape") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Arial, 9.75pt, style=Bold")>  _
        Public Property SubHeaderFont() As Global.System.Drawing.Font
            Get
                Return CType(Me("SubHeaderFont"),Global.System.Drawing.Font)
            End Get
            Set
                Me("SubHeaderFont") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.belfer.NET.My.MySettings
            Get
                Return Global.belfer.NET.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace