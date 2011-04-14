function ms () { 
    if($args[0] -eq $null) {
       ls *.sln | % {C:/Windows/Microsoft.NET/Framework/v4.0.30319/MSBuild.exe $_.Name}
    } else {
        C:/Windows/Microsoft.NET/Framework/v4.0.30319/MSBuild.exe $args[0]
    }
}

