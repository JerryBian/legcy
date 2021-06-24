### Introduction

Handy assistant for managing private system, well I mean the system named **laobian**.

At the core, it can achieve:

- Manage blogs, such as adding/updating posts, in batch or not
- Upload new file, as well as listing
- More will be added if necessary in the future

It's .NET global tool, so that it's corss platform in nature. As command line tool, it is user friendly thanks to the great lirary [CommandLineParser](https://github.com/commandlineparser/commandline).

### Usage

Make sure you have latest [dotnet core](https://dotnet.microsoft.com/download) installed.

#### Install tool

```sh
dotnet tool install --global Laobian.Jarvis
```

Exposed command name is `laobian`.

#### Add Microsoft Azure storage connection

Azure storage blob is backend store, so we need to supply connection string in order to continue to use Jarvis.

```sh
# replace value to Azure Storage connection string
laobian config -n "AzureStorageConnection" -v "UseDevelopmentStorage=true"
```

### Commands

#### `post`

This subcommand is designed for managing blog posts in [Jerry Bian's blog](https://blog.laobian.me).

**Value not bind to option**: file path or folder path, relative(to current executing root) or absolute. If it's not specified, current executing root folder will be the value.

| Short name      | Long name |  Explain | Comment |
| :-----------: | :-----------: | :-----------: | :-----------: |
| -n | --new | Create new post. Unbounded value must be valid folder path, this command will create new post under specified folder. | Assign new post id automatically. |
| -a      | --add       | Add new post(s). If value is `.md` extension, add as single post. If value is valid folder path, add posts in files with `.md` extension under specified folder path.| If post already exists, return error. |
| -u   | --update        | Update existing post(s). If value is `.md` extension, update as single post. If value is valid folder path, update posts in files with `.md` extension under specified folder path.| If post does not exist, return error. |
| -s   | --silent        | Keep silent if single post adding/updating failed. This is useful under batch operations, one post failure will not prevent remaining posts processing.| `True` in default. |

> Examples:
> ```sh
> laobian post -n # create new empty post in current folder
> laobian post -a "post.md" # add new post "post.md" which is in current folder
> laobian post -u "/tmp/posts" -s false # update posts under "/tmp/posts" folders in batch mode, if one post failed, process returned with error.
> ```

#### `file`

This subcommand is designed for managing files in locally and remotely.

**Value not bind to option**: file path or folder path, relative(to current executing root) or absolute. If it's not specified, current executing root folder will be the value.

| Short name      | Long name |  Explain | Comment |
| :-----------: | :-----------: | :-----------: | :-----------: |
| -l | --list | List all files in store with metadata information displayed. |
| -a      | --add       | Add new file. Unbounded value must be valid file path, this command will add this file to remote store. | Full HTTP path will be returned if succeed. Prompt to replace or terminate if same file name already exist. |
| -d   | --download        | Download all remote files. Unbounded value must be valid folder path, this command will download all files from remote store to local. | Override if file already exist locally. |

> Examples:
> ```sh
> laobian file -l # list all remote files metadata
> laobian file -a "joe.jpeg" # add local file "joe.jpeg" in current folder to store
> laobian file -d "/tmp/files" # download all remoted files and save them at "/tmp/files"
> ```

#### `config`

This subcommand is designed for manginging configurations in locally.

| Short name      | Long name |  Explain | Comment |
| :-----------: | :-----------: | :-----------: | :-----------: |
| -l | --list | List all configurations locally. |
| -n      | --name       | Configuration name, used with `-v` together(__not required__). Add or update configuration. | If `-l` was specified with `-n`, configuration value will be returned. |
| -v   | --value        | Configuration value, used with `-n` together(__required__). Add or update configuration. |  |

> Examples:
> ```sh
> laobian config -l # list all configurations with name and value
> laobian config -n "AzureStorageConnection" # add or update configuration "AzureStorageConnection" locally
> ```

### License

```
MIT License

Copyright (c) 2018 Jerry Bian

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
