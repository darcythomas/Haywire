#!/bin/bash

echo Start deploy

echo Kill old
pkill haywire_hello_w

echo Start the new
( $PWD/builds/unix/debug/haywire_hello_world & ) 

echo done