use rnet::{net};

rnet::root!();

#[net]
fn say_hello(name: &str) {
    println!("Hello, {}!", name);
}