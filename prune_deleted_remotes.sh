git branch -vv | grep ': gone]'|  grep -v "\*" | awk '{ print $1; }' | xargs -r git branch -d -f

git config --global alias.cleanup '!git branch -vv | grep ": gone]" | grep -v "\*" | awk "{ print \$1; }" | xargs -r git branch -d -f'
