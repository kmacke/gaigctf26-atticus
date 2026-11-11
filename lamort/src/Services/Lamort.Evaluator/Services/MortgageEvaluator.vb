Imports System.Globalization

Namespace Services
    Public NotInheritable Class MortgageEvaluator
        Private Shared ReadOnly AmountCulture As CultureInfo = CultureInfo.GetCultureInfo("en-US")

        ' Amounts are user-supplied strings. They are parsed as numbers here and are
        ' never interpolated into code or a command line.
        Private Shared Function ParseAmount(value As String) As Decimal
            Dim parsed As Decimal
            If Not Decimal.TryParse(value.Trim(), NumberStyles.AllowDecimalPoint Or NumberStyles.AllowCurrencySymbol, AmountCulture, parsed) Then
                Throw New InvalidOperationException("Application contains a non-numeric amount.")
            End If
            Return parsed
        End Function

        Public Shared Function Evaluate(application As MortgageApplicationSnapshot) As EvaluationResult
            Dim score As Decimal = 0D

            ' ricks eval algorithm
            ' does the job more or less
            For Each income In application.Incomes
                If income <> "" Then
                    score += 5D * ParseAmount(income)
                End If
            Next
            For Each asset In application.Assets
                If asset <> "" Then
                    score += ParseAmount(asset)
                End If
            Next
            For Each debt In application.Debts
                If debt <> "" Then
                    score -= 2D * ParseAmount(debt)
                End If
            Next

            Dim requiredScore As Decimal = 2D
            If application.HomePriceMax.HasValue Then
                Dim homePrice = application.HomePriceMax.Value
                If homePrice <= 50000D Then
                    requiredScore = 2D
                ElseIf homePrice >= 2000000D Then
                    requiredScore = 4D
                Else
                    requiredScore = 2D + ((homePrice - 50000D) * 2D / 1950000D)
                End If
            End If

            If score >= requiredScore Then
                Return New EvaluationResult(True, "Approved! Keep it real... estate!")
            End If

            Return New EvaluationResult(False, "Denied! Better luck next time.")
        End Function
    End Class

    Public NotInheritable Class PerlEvaluationOutput
        Public Property Approved As Boolean

        Public Property Reason As String
    End Class

    Public NotInheritable Class EvaluationResult
        Public Sub New(approved As Boolean, reason As String)
            Me.Approved = approved
            Me.Reason = reason
        End Sub

        Public ReadOnly Property Approved As Boolean

        Public ReadOnly Property Reason As String
    End Class
End Namespace
