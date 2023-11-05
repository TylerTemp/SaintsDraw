# SaintsDraw #

## Development ##

1.  `git clone --branch unity ${your-folk-git-url} SaintsDraw`
2.  `git rm Assets/SaintsDraw`
3.  `rm -rf .git/modules/SaintsDraw`
4.  `git config --remove-section submodule.Assets/SaintsDraw`
5.  `git submodule add --force ${your-folk} Assets/SaintsDraw`
6.  `cd Assets/SaintsDraw` and checkout to your editing branch, e.g. `git checkout master`
7.  windows: `.\link.cmd`

    mac/linux: `ln -s 'Assets/SaintsDraw/Samples~' 'Assets/SaintsDraw/Samples'`

