$dir = "/tools/mongodb"

if($args[0].equals("start")) {
    & "$dir/bin/mongod.exe" --dbpath "$dir/data"
} elseif ($args[0].equals("shell")) {
    & "$dir/bin/mongo.exe" 
} 
