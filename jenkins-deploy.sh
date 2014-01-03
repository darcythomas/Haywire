#!/bin/bash


echo Start deploy

#./haywire.sh stop
#./haywire.sh start


pkill haywire_hello_w

( $PWD/builds/unix/debug/haywire_hello_world & ) 

echo done