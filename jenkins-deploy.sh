#!/bin/bash


echo Start deploy

#./haywire.sh stop
#./haywire.sh start

( $PWD/builds/unix/debug/haywire_hello_world & ) disown

echo done