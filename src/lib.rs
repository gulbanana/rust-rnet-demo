#![feature(core_intrinsics)]
use std::intrinsics::unchecked_add;
use rnet::{net};
rnet::root!();

static mut NUMBERS: Vec<i32> = Vec::new();

#[net]
unsafe fn set_numbers(values: &[i32]) {
    NUMBERS = values.to_vec();
}

#[net]
unsafe fn add_loop() -> i32 {
    let mut accum = 0;
    for v in NUMBERS.iter() {
        accum += v;
    }
    return accum;
}

#[net]
unsafe fn add_fold() -> i32 {
    return NUMBERS.iter().fold(0, |a, b| unchecked_add(a, *b));
}