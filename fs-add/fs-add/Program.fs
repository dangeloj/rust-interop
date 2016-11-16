open System
open System.Runtime.InteropServices

/// A Vector3 struct that matches our Rust Vector3
[<Struct; StructLayout(LayoutKind.Sequential)>]
type Vector3 =
    val x: int
    val y: int
    val z: int
    new(x, y, z) = { x = x; y = y; z = z }

    override this.ToString() =
        sprintf "<%i,%i,%i>" this.x this.y this.z

[<AutoOpen>]
module ExternalCalls =
    [<DllImport("add.dll", EntryPoint = "add", CallingConvention = CallingConvention.Cdecl)>]
    extern int32 add(int32, int32)

    [<DllImport("add.dll", EntryPoint = "vec3_add", CallingConvention = CallingConvention.Cdecl)>]
    extern Vector3 vec3_add(Vector3, Vector3)

    [<DllImport("add.dll", EntryPoint = "vec3_magnitude", CallingConvention = CallingConvention.Cdecl)>]
    extern float vec3_magnitude(Vector3)

    [<DllImport("add.dll", EntryPoint = "vec3_new", CallingConvention = CallingConvention.Cdecl)>]
    extern IntPtr vec3_new(int, int, int)

    [<DllImport("add.dll", EntryPoint = "vec3_print_values", CallingConvention = CallingConvention.Cdecl)>]
    extern void vec3_print_values(IntPtr)

    [<DllImport("add.dll", EntryPoint = "vec3_set_values", CallingConvention = CallingConvention.Cdecl)>]
    extern void vec3_set_values(int, int, int, IntPtr)

    [<DllImport("add.dll", EntryPoint = "vec3_del", CallingConvention = CallingConvention.Cdecl)>]
    extern void vec3_del(IntPtr)

let addNums() =
    let x, y = 1, 1
    let z = add(x, y)

    printfn "%i + %i = %i" x y z

let addVecs() =
    let v1, v2 =
        Vector3(1, 1, 1),
        Vector3(2, 2, 2)

    let v3 =
        vec3_add(v1, v2)

    printfn "%A + %A = %A" v1 v2 v3

let getMagnitude() =
    let vec = Vector3(0, 3, 0)
    let mag = vec3_magnitude(vec)
    printfn "Magnitude of %A is %f" vec mag

let usePointers() =
    let vec3Ptr = vec3_new(1, 2, 3)
    vec3_print_values(vec3Ptr)
    vec3_set_values(4, 5, 6, vec3Ptr)
    vec3_print_values(vec3Ptr)
    vec3_del(vec3Ptr)

[<EntryPoint>]
let main argv =
    Console.WriteLine("Calling into Rust from F#\n\n")

    addNums()
    addVecs()
    getMagnitude()
    usePointers()
    0 // return an integer exit code