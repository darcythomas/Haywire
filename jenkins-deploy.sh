#!/bin/bash


echo Start deploy


pkill haywire_hello_w

( ./builds/unix/debug/haywire_hello_world &)

echo done