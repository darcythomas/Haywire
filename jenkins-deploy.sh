#!/bin/bash

echo Start deploy

git pull

$PWD/build.sh


echo Kill old
pkill haywire_hello_w

echo Start the new
( $PWD/builds/unix/debug/haywire_hello_world & )

echo done