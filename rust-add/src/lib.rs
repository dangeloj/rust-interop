#[repr(C)]
#[derive(Debug)]
pub struct Vector3 {
    x: i32,
    y: i32,
    z: i32,
} 

#[no_mangle]
pub extern "C" fn get_num() -> i32 {
    42
}

#[no_mangle]
pub extern fn add(x: i32, y: i32) -> i32 {
    x + y
}

#[no_mangle]
pub extern "C" fn vec3_add(v1: &Vector3, v2: &Vector3) -> Vector3 {
    Vector3 {
        x: v1.x + v2.x,
        y: v1.y + v2.y,
        z: v1.z + v2.z,
    }
}

#[no_mangle]
pub extern "C" fn vec3_magnitude(v: &Vector3) -> f64 {
    let sum = v.x * v.x + v.y * v.y + v.z * v.z; 
    (sum as f64).sqrt()
}

#[no_mangle]
pub unsafe extern "C" fn vec3_new(x: i32, y: i32, z: i32) -> *const Vector3 {
    println!("Creating vector <{},{},{}> in Rust", x, y, z);
    let boxxed = Box::new(Vector3 {
        x: x, y: y, z: z
    });

    Box::into_raw(boxxed)
}

#[no_mangle]
pub unsafe extern "C" fn vec3_set_values(x: i32, y: i32, z: i32, target: *mut Vector3) {
    println!("Setting vector to <{},{},{}> in Rust", x, y, z);
    let mut vec = &mut *target;
    vec.x = x;
    vec.y = y;
    vec.z = z;
}

#[no_mangle]
pub unsafe extern "C" fn vec3_print_values(input: *const Vector3) {
    let v = &*input;
    println!("Values from Rust: {:?}", v);
}

#[no_mangle]
pub unsafe extern "C" fn vec3_del(vec: *mut Vector3) {
    println!("Deleting vector in Rust");
    let _ = Box::from_raw(vec);
}