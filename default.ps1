include build-utils.ps1

properties {
    $dir = "/tools/mongodb"
    $nunitDir = "D:\Work\Libs\nunit\net-2.0"
}

task default -depends all

task all -depends compile, test {

}

task compile {
    ms 'LittleProblem/LittleProblem.sln' 
}

task test -depends compile {
    & "$nunitDir\nunit-console.exe" 'LittleProblem/LittleProblem.CommonTest/bin/Debug/LittleProblem.CommonTest.dll' /nologo
    & "$nunitDir\nunit-console.exe" 'LittleProblem/LittleProblem.DataTest/bin/Debug/LittleProblem.DataTest.dll' /nologo

}

task mongo-start {
    if(ps | Where {$_.Name -eq "mongod"}) {} else {
        & "$dir/bin/mongod.exe" --dbpath "$dir/data"
    }
} 

task mongo-shell {
    & "$dir/bin/mongo.exe"
}


