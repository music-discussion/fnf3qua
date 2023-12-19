# fnf3qua

This is a "reverse-engineer" of [qua3osu](https://github.com/IceDynamix/qua3osu)
to convert FNF Json files into Quaver files.

## Download

[Download here](https://github.com/music-discussion/fnf3qua/releases/latest)

## Run with Drag&Drop

https://user-images.githubusercontent.com/22303902/140838272-28793396-ce55-42d4-a69f-f2f90f6187a5.mp4

## Run through the command line

Run the project with the `--help` flag to get everything you need. If you
downloaded from releases, open up a command line and run `fnf3qua.exe --help`.

```
fnf3qua 1.0.0
Copyright (C) 2020 qua3osu

  -o, --output                   Specifies the output directory, uses original directory of .qp by default

  -c, --creator                  Changes the creator username for all maps

  --help                         Display this help screen.

  --version                      Display version information.

  input-paths (pos. 0)           Required. Path(s) to directories containing .qp files or direct .qp file path(s)
```

Example commands:

Regular conversion of a mapset file called my-mapset.json: `<path>/fnf3qua.exe my-mapset.json`
With the creator name changed to Discussions: `<path>/fnf3qua.exe my-mapset.json --creator Discussions`
