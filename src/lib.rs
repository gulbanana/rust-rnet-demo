#![feature(core_intrinsics)]
use std::{sync::Arc};
use rnet::{net};
rnet::root!();

pub struct State {
    numbers: Vec<i32>
}

impl State {
    #[net]
    pub fn init_state(values: &[i32]) -> Arc<State> {
        Arc::new(State { numbers: values.to_vec() })
    }
}

#[net]
pub fn add_loop(state: Arc<State>) -> i32 {
    let mut accum = 0;
    for v in state.numbers.iter() {
        accum += v;
    }
    return accum;
}

#[net]
pub fn add_fold(state: Arc<State>) -> i32 {
    return state.numbers.iter().fold(0, |a, b| a + *b);
}