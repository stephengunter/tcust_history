'////////////////////////////////////////////////////////////////////////
' Copyright 2001-2013 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.Pdf. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'////////////////////////////////////////////////////////////////////////

Imports Microsoft.VisualBasic
Imports System.IO

Imports Aspose.Pdf
Imports Aspose.Pdf.InteractiveFeatures

Namespace GetZoomFactor
	Public Class Program
		Public Shared Sub Main(ByVal args() As String)
			' The path to the documents directory.
			Dim dataDir As String = Path.GetFullPath("../../../Data/")

			' instantiate new Document object
			Dim doc As New Document(dataDir & "Zoomed_pdf.pdf")

			' create GoToAction object
			Dim action As GoToAction = TryCast(doc.OpenAction, GoToAction)

			' get the Zoom factor of PDF file
			System.Console.WriteLine((TryCast(action.Destination, XYZExplicitDestination)).Zoom) ' Document zoom value;
		End Sub
	End Class
End Namespace