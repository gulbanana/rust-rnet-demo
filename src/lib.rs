#![feature(core_intrinsics)]
use std::intrinsics::unchecked_add;

use rnet::{net};

rnet::root!();

#[net]
fn say_hello(name: &str) {
    println!("Hello, {}!", name);
}

#[net]
unsafe fn add_numbers(values: &[i32]) -> i32 {
    return values.iter().fold(0, |a, b| unchecked_add(a, *b));
}