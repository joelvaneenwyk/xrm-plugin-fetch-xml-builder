#!/bin/bash

submodules="$(git config --file .gitmodules --get-regexp path | awk '{ print $2 }')"

for submodule in $submodules; do
    (
        cd "$submodule" || true
        owner=$(git config --get remote.origin.url | sed 's/.*:\/\/github.com\///' | sed 's/\.git$//' | cut -d/ -f1)
        repo=$(git config --get remote.origin.url | sed 's/.*:\/\/github.com\///' | sed 's/\.git$//' | cut -d/ -f2)
        gh repo fork "$owner/$repo" --clone=false
    )
done
