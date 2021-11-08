namespace FsharpDesktop


module Simulation = 

    
    open MathNet.Numerics.LinearAlgebra
    type Point = {Mass:float;mutable Position:Vector<float>;mutable Velocity:Vector<float>;mutable Acceleration:Vector<float>}
        
    let g = vector[0.0;-1.]
    
    let Point1 = {Mass = 1.0;Position = vector[100.;0.];Velocity = vector[0.;0.];Acceleration = vector[0.;0.];}
    let Point2 = {Mass = 1.0;Position = vector[200.;0.]; Velocity = vector[0.;0.];Acceleration = vector[0.;0.];}
    
    let joinLine1 = 100.0
    let joinLine2 = 100.0
    
    let k1 = 0.1
    let k2 = 0.1
    
    //aprocsimation
    let Delta = 20.
    
    let Dinstance (vector1) vector2 = vector2 - vector1
    
    
    let  ForceCalcPoint1 point1 point2  = 
        // T натяжение
        // Norm длина
        let gravitationforce = point1.Mass * g
        let T1 =  - point1.Position.Normalize(2.)* k1 *(point1.Position.Norm(2.)-joinLine1) 
        let T2 = k2 * (Dinstance point1.Position point2.Position).Normalize(2.) * ((Dinstance point1.Position point2.Position).Norm(2.) - joinLine1) 
        gravitationforce + T1 + T2
    
    let  ForceCalcPoint2 point1 point2 = 
        let gravitationforce = point2.Mass * g
        let T2 = k2 * (Dinstance point2.Position point1.Position).Normalize(2.) * ((Dinstance point1.Position point2.Position).Norm(2.) - joinLine2) 
        gravitationforce + T2
    
    let AccelerationCalc (allForce : Vector<float>) point = allForce/point.Mass
    
    let VelocityCalc point = point.Velocity + point.Acceleration/Delta

    let PositionCalc point = point.Position + point.Velocity/Delta

    let SimLoop1 point = 
        let mutable a1 = ForceCalcPoint1 Point1 Point2
        point.Acceleration  <-  AccelerationCalc a1 Point1
        point.Velocity <- VelocityCalc Point1
        point.Position <- PositionCalc Point1


    let Simloop2 point =
        let mutable a2 = ForceCalcPoint2 Point1 Point2
        point.Acceleration  <-  AccelerationCalc a2 Point2
        point.Velocity <- VelocityCalc Point2
        point.Position <- PositionCalc Point2

    